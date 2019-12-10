using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Lexer
{

    public class LexerException : System.Exception
    {
        public LexerException(string msg)
            : base(msg)
        {
        }

    }

    public class Lexer
    {

        protected int position;
        protected char currentCh; // очередной считанный символ
        protected int currentCharValue; // целое значение очередного считанного символа
        protected System.IO.StringReader inputReader;
        protected string inputString;

        public Lexer(string input)
        {
            inputReader = new System.IO.StringReader(input);
            inputString = input;
        }

        public void Error()
        {
            System.Text.StringBuilder o = new System.Text.StringBuilder();
            o.Append(inputString + '\n');
            o.Append(new System.String(' ', position - 1) + "^\n");
            o.AppendFormat("Error in symbol {0}", currentCh);
            throw new LexerException(o.ToString());
        }

        protected void NextCh()
        {
            this.currentCharValue = this.inputReader.Read();
            this.currentCh = (char) currentCharValue;
            this.position += 1;
        }

        public virtual bool Parse()
        {
            return true;
        }

        public bool ReachEnd()
        {
            return currentCharValue == -1;
        }

        public bool IsCurrentCharSign()
        {
            return currentCh == '+' || currentCh == '-';
        }

        public bool IsCurrentCharSign(out int sign)
        {
            sign = 1;

            if (currentCh == '-')
            {
                sign = -1;
            }

            return currentCh == '+' || currentCh == '-';
        }
    }

    public class IntLexer : Lexer
    {

        protected System.Text.StringBuilder intString;
        public int parseResult = 0;
        protected int sign = 1;

        public IntLexer(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override bool Parse()
        {
            NextCh();
            if (IsCurrentCharSign(out sign))
            {
                NextCh();
            }
        
            if (char.IsDigit(currentCh))
            {
                parseResult = (int)char.GetNumericValue(currentCh);
                NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsDigit(currentCh))
            {
                parseResult *= 10;
                parseResult += (int)char.GetNumericValue(currentCh);

                NextCh();
            }

            parseResult *= sign;

            if (!ReachEnd())
            {
                Error();
            }

            return true;

        }
    }
    
    public class IdentLexer : Lexer
    {
        private string parseResult;
        protected StringBuilder builder;
    
        public string ParseResult
        {
            get { return parseResult; }
        }
    
        public IdentLexer(string input) : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            var findAtLeastOneSymbol = false;
            NextCh();

            while (currentCh == '_' || char.IsLetterOrDigit(currentCh))
            {
                builder.Append(currentCh);
                NextCh();

                findAtLeastOneSymbol = true;
            }

            if (!findAtLeastOneSymbol)
            {
                Error();
            }

            if (!ReachEnd())
            {
                Error();
            }

            parseResult = builder.ToString();
            return true;
        }
       
    }

    public class IntNoZeroLexer : IntLexer
    {
        public IntNoZeroLexer(string input)
            : base(input)
        {
        }

        public override bool Parse()
        {
            NextCh();

            if (IsCurrentCharSign(out sign))
            {
                NextCh();
            }

            if (char.IsDigit(currentCh) && currentCh != '0')
            {
                NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsDigit(currentCh) &&  char.GetNumericValue(currentCh) == 0 && !ReachEnd())
            {
                NextCh();
            }

            if (ReachEnd())
            {
                Error();
                return false;
            }

            base.Parse();

            return true;
        }
    }

    public class LetterDigitLexer : Lexer
    {
        protected StringBuilder builder;
        protected string parseResult;

        public string ParseResult
        {
            get { return parseResult; }
        }

        public LetterDigitLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            var findSymbol = false;
            NextCh();

            while (!ReachEnd())
            {
                if (char.IsLetter(currentCh))
                {
                    builder.Append(currentCh);
                    NextCh();
                    findSymbol = true;
                }
                else
                {
                    break;
                }

                if (ReachEnd())
                {
                    break;
                }

                if (char.IsDigit(currentCh))
                {
                    builder.Append(currentCh);
                    NextCh();
                }
                else
                { 
                    break; 
                }
            }

            if (!findSymbol)
            {
                Error();
            }

            if (!ReachEnd())
            {
                Error();
            }

            parseResult = builder.ToString();
            return true;
        }
       
    }

    public class LetterListLexer : Lexer
    {
        protected List<char> parseResult;

        public List<char> ParseResult
        {
            get { return parseResult; }
        }

        public LetterListLexer(string input)
            : base(input)
        {
            parseResult = new List<char>();
        }

        public override bool Parse()
        {
            var findSymbol = false;
            NextCh();

            while (!ReachEnd())
            {
                if (char.IsLetter(currentCh))
                {
                    parseResult.Add(currentCh);
                    NextCh();
                    findSymbol = true;
                } 
                else
                {
                    break;
                }

                if (ReachEnd())
                {
                    break;
                }

                if (currentCh == ',' || currentCh == ';')
                {
                    if (position == inputString.Length)
                    {
                        break;
                    }

                    NextCh();
                } 
                else
                {
                    break;
                }
            }

            if (!findSymbol || !ReachEnd())
            {
                Error();
            }

            return true;
        }
    }

    public class DigitListLexer : Lexer
    {
        protected List<int> parseResult;

        public List<int> ParseResult
        {
            get { return parseResult; }
        }

        public DigitListLexer(string input)
            : base(input)
        {
            parseResult = new List<int>();
        }

        public override bool Parse()
        {
            var findSymbol = false;
            var needSpace = false;
            NextCh();

            while (!ReachEnd())
            {
                while (char.IsWhiteSpace(currentCh))
                {
                    if (!findSymbol)
                    {
                        Error();
                    }

                    needSpace = false;
                    NextCh();

                    if (ReachEnd())
                    {
                        Error();
                    }
                }

                if (ReachEnd())
                {
                    break;
                }

                if (needSpace)
                {
                    break;
                }

                if (char.IsDigit(currentCh))
                {
                    parseResult.Add((int)char.GetNumericValue(currentCh));
                    NextCh();
                    findSymbol = true;
                }

                needSpace = true;
            }

            if (!findSymbol || !ReachEnd())
            {
                Error();
            }

            return true;
        }
    }

    public class LetterDigitGroupLexer : Lexer
    {
        protected StringBuilder builder;
        protected string parseResult;

        public string ParseResult
        {
            get { return parseResult; }
        }
        
        public LetterDigitGroupLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            NextCh();

            if (ReachEnd())
            {
                Error();
            }

            while (!ReachEnd())
            {
                if (char.IsLetter(currentCh))
                {
                    builder.Append(currentCh);
                    NextCh();
                }
                else
                {
                    break;
                }

                if (ReachEnd())
                {
                    break;
                }

                if (char.IsLetter(currentCh))
                {
                    builder.Append(currentCh);
                    NextCh();
                }

                if (ReachEnd())
                {
                    break;
                }

                if (char.IsDigit(currentCh))
                {
                    builder.Append(currentCh);
                    NextCh();
                }
                else
                {
                    break;
                }

                if (ReachEnd())
                {
                    break;
                }

                if (char.IsDigit(currentCh))
                {
                    builder.Append(currentCh);
                    NextCh();
                }
            }

            if (!ReachEnd())
            {
                Error();
            }

            parseResult = builder.ToString();
            return true;
        }
       
    }

    public class DoubleLexer : Lexer
    {
        private StringBuilder builder;
        private double parseResult;

        public double ParseResult
        {
            get { return parseResult; }

        }

        public DoubleLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            NextCh();

            if (ReachEnd())
            {
                Error();
            }

            var left = 0;
            var findLeft = false;

            while (char.IsDigit(currentCh) && !ReachEnd())
            {
                left += (int)char.GetNumericValue(currentCh);
                NextCh();
                findLeft = true;

                if (char.IsDigit(currentCh) && !ReachEnd())
                {
                    left *= 10;
                }
            }

            var findDelimeter = false;

            if (currentCh == '.')
            {
                NextCh();
                findDelimeter = true;
            }

            if ((findDelimeter && ReachEnd()) || !findLeft)
            {
                Error();
            }

            var right = 0;
            var i = 10; 

            while (char.IsDigit(currentCh) && !ReachEnd())
            {
                right += (int)char.GetNumericValue(currentCh);
                NextCh();

                if (!ReachEnd())
                {
                    right *= 10;
                    i *= 10;
                }
            }

            parseResult = left + right / (double)i;

            return true;
        }
       
    }

    public class StringLexer : Lexer
    {
        private StringBuilder builder;
        private string parseResult;

        public string ParseResult
        {
            get { return parseResult; }

        }

        public StringLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            NextCh();

            if (currentCh == '\'')
            {
                NextCh();
            } 
            else
            {
                Error();
            }

            while (position < inputString.Length)
            {
                if (currentCh == '\'')
                {
                    Error();
                }

                NextCh();
            }

            if (currentCh == '\'')
            {
                NextCh();
            }
            else
            {
                Error();
            }

            return true;
        }
    }

    public class CommentLexer : Lexer
    {
        private StringBuilder builder;
        private string parseResult;

        public string ParseResult
        {
            get { return parseResult; }

        }

        public CommentLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
        }

        public override bool Parse()
        {
            NextCh();

            if (currentCh == '/')
            {
                NextCh();
            }
            else
            {
                Error();
            }

            if (currentCh == '*')
            {
                NextCh();
            }
            else
            {
                Error();
            }

            while (position < inputString.Length - 1)
            {
                var possibleError = false;

                if (currentCh == '*')
                {
                    possibleError = true;
                }

                NextCh();

                if (currentCh == '/' && possibleError)
                {
                    Error();
                }
            }

            if (currentCh == '*')
            {
                NextCh();
            }
            else
            {
                Error();
            }

            if (currentCh == '/')
            {
                NextCh();
            }
            else
            {
                Error();
            }

            return true;
        }
    }

    public class IdentChainLexer : Lexer
    {
        private StringBuilder builder;
        private List<string> parseResult;

        public List<string> ParseResult
        {
            get { return parseResult; }

        }

        public IdentChainLexer(string input)
            : base(input)
        {
            builder = new StringBuilder();
            parseResult = new List<string>();
        }

        public override bool Parse()
        {
            NextCh();
            var neededDots = 0;
            
            if (currentCh == 'u')
            {
                neededDots = 2;
            } 
            else if (currentCh == 'I')
            {
                neededDots = 0;
            } 
            else
            {
                Error();
            }



            return true;
        }
    }

    public class Program
    {
        public static void Main()
        {
            string input = "1 2";
            Lexer L = new LetterDigitGroupLexer("aa12ab23");
            try
            {
                L.Parse();
            }
            catch (LexerException e)
            {
                System.Console.WriteLine(e.Message);
            }

        }
    }
}