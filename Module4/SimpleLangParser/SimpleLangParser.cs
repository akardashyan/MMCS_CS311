using System;
using System.Collections.Generic;
using System.Text;
using SimpleLexer;
namespace SimpleLangParser
{
    public class ParserException : System.Exception
    {
        public ParserException(string msg)
            : base(msg)
        {
        }

    }

    public class Parser
    {
        private SimpleLexer.Lexer l;

        public Parser(SimpleLexer.Lexer lexer)
        {
            l = lexer;
        }

        public void Progr()
        {
            Block();
        }

        public void Expr()
        {
            CheckIdNumBrackets();
            CheckMultDivision();
            CheckPlusMinus();
        }

        public void CheckIdNumBrackets()
        {
            // Если ID или число, берем следующую лексему
            if (l.LexKind == Tok.ID || l.LexKind == Tok.INUM)
            {
                l.NextLexem();
            }
            // Если текущая лексема левая скобка
            else if (l.LexKind == Tok.LEFT_BRACKET)
            {
                // Переходим к следующей лексеме
                l.NextLexem();
                // Находим все лексемы до правой скобки
                Expr();

                // Если нашли правую скобку, 
                // то переходим к след лексеме
                // иначе ошибка
                if (l.LexKind == Tok.RIGHT_BRACKET)
                {
                    l.NextLexem();
                }
                else
                {
                    SyntaxError(") expected");
                }
            }
            else
            { 
                SyntaxError("expression expected"); 
            }
        }

        public void CheckPlusMinus()
        {
            // Если текущая лексема + -
            while (l.LexKind == Tok.PLUS || l.LexKind == Tok.MINUS)
            {
                // Переходим след лексема
                l.NextLexem();
                // Проверяем след лексему на число или ид
                CheckIdNumBrackets();
                // Проверяем след лексему на * /
                CheckMultDivision();
                // Проверяем след лексему на + -
                CheckPlusMinus();
            }
        }

        public void CheckMultDivision()
        {
            // Если текущая лексема * /
            while (l.LexKind == Tok.MULT || l.LexKind == Tok.DIVISION)
            {
                // Переходим к след лексеме
                l.NextLexem();
                // Проверяем на число и ид
                CheckIdNumBrackets();
                // Проверяем на лексему * /
                CheckMultDivision();
            }
        }

        public void Assign()
        {
            l.NextLexem();  // пропуск id
            if (l.LexKind == Tok.ASSIGN)
            {
                l.NextLexem();
            }
            else
            {
                SyntaxError(":= expected");
            }
            Expr();
        }

        public void StatementList()
        {
            Statement();
            while (l.LexKind == Tok.SEMICOLON)
            {
                l.NextLexem();
                Statement();
            }
        }

        public void Statement()
        {
            switch (l.LexKind)
            {
                case Tok.BEGIN:
                    {
                        Block();
                        break;
                    }
                case Tok.CYCLE:
                    {
                        Cycle();
                        break;
                    }
                case Tok.ID:
                    {
                        Assign();
                        break;
                    }
                case Tok.FOR:
                    {
                        For();
                        break;
                    }
                default:
                    {
                        SyntaxError("Operator expected");
                        break;
                    }
            }
        }

        public void Block()
        {
            l.NextLexem();    // пропуск begin
            StatementList();
            if (l.LexKind == Tok.END)
            {
                l.NextLexem();
            }
            else
            {
                SyntaxError("end expected");
            }
        }

        public void Cycle()
        {
            l.NextLexem();  // пропуск cycle
            Expr();
            Statement();
        }

        public void For()
        {
            l.NextLexem();
            Assign();

            // Ищем лексему To
            if (l.LexKind == Tok.TO)
            {
                l.NextLexem();
            }
            else
            { 
                SyntaxError("to expected"); 
            }
            
            // Обрабатываем условие цикла
            Expr();

            // Ищем лексему DO
            if (l.LexKind == Tok.DO)
            {
                l.NextLexem();
            }
            else
            { 
                SyntaxError("do expected"); 
            }
            
            // Обрабатываем тело цикла
            Statement();
        }

        public void SyntaxError(string message)
        {
            var errorMessage = "Syntax error in line " + l.LexRow.ToString() + ":\n";
            errorMessage += l.FinishCurrentLine() + "\n";
            errorMessage += new String(' ', l.LexCol - 1) + "^\n";
            if (message != "")
            {
                errorMessage += message;
            }
            throw new ParserException(errorMessage);
        }

    }
}
