%{
// Ёти объ€влени€ добавл€ютс€ в класс GPPGParser, представл€ющий собой парсер, генерируемый системой gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token BEGIN END CYCLE INUM RNUM ID ASSIGN SEMICOLON WHILE DO REPEAT UNTIL FOR TO IF THEN ELSE 
       VAR COMMA WRITE PLUS MINUS MULTIPLICATION DIVISION LEFT_BRACKET RIGHT_BRACKET 

%%

progr   : block
		;

stlist	: statement 
		| stlist SEMICOLON statement 
		;

statement: assign
		| block  
		| cycle  
		| while
        | repeat
  		| for
        | write
        | if
        | idents
		;

ident 	: ID 
		;
	
assign 	: ident ASSIGN expr 
		;

expr	: ident  
		| INUM 
		;

block	: BEGIN stlist END 
		;

cycle	: CYCLE expr statement 
		;
	
while   : WHILE expr DO statement
        ;
       
repeat  : REPEAT stlist UNTIL expr 
		;

for     : FOR assign TO expr DO statement
		;

write   : WRITE LEFT_BRACKET expr RIGHT_BRACKET
		;

if      : IF expr THEN statement
		| IF expr THEN statement ELSE statement
		;

idents  : VAR ident 
		| idents COMMA ident 
		;

expr    : T
        | expr PLUS T
        | expr MINUS T
        ;

T       : F
        | T MULTIPLICATION F
        | T DIVISION F
        ;

F       : ident
        | INUM 
        | LEFT_BRACKET expr RIGHT_BRACKET
        ;			
%%
