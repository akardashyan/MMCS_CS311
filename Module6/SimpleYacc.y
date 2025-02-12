%{
// ��� ���������� ����������� � ����� GPPGParser, �������������� ����� ������, ������������ �������� gppg
    public BlockNode root; // �������� ���� ��������������� ������ 
    public Parser(AbstractScanner<ValueType, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%union { 
			public double dVal; 
			public int iVal; 
			public string sVal; 
			public Node nVal;
			public ExprNode eVal;
			public StatementNode stVal;
			public BlockNode blVal;
       }

%using ProgramTree;

%namespace SimpleParser

%token BEGIN END CYCLE ASSIGN SEMICOLON COMMA PLUS MINUS MULT DIVISION LEFT_BRACKET RIGHT_BRACKET WHILE DO WRITE VAR
%token <iVal> INUM 
%token <dVal> RNUM 
%token <sVal> ID

%type <eVal> expr ident T F
%type <stVal> assign statement cycle while write var varlist 
%type <blVal> stlist block

%%

progr   : block { root = $1; }
		;

stlist	: statement 
			{ 
				$$ = new BlockNode($1); 
			}
		| stlist SEMICOLON statement 
			{ 
				$1.Add($3); 
				$$ = $1; 
			}
		;

statement: assign { $$ = $1; }
		| block   { $$ = $1; }
		| cycle   { $$ = $1; }
		| var	  { $$ = $1; }
		| while	  { $$ = $1; }
		| write	  { $$ = $1; }
	;

ident 	: ID { $$ = new IdNode($1); }	
		;
	
assign 	: ident ASSIGN expr { $$ = new AssignNode($1 as IdNode, $3); }
		;
		
expr	: expr PLUS T { $$ = new BinaryNode($1,$3,'+'); }
		| expr MINUS T { $$ = new BinaryNode($1,$3,'-'); }
		| T { $$ = $1; }
		;

block	: BEGIN stlist END { $$ = $2; }
		;

cycle	: CYCLE expr statement { $$ = new CycleNode($2, $3); }
		;
	
T 		: T MULT F { $$ = new BinaryNode($1,$3,'*'); }
		| T DIVISION F { $$ = new BinaryNode($1,$3,'/'); }
		| F { $$ = $1; }
		;
		
F 		: ident  { $$ = $1 as IdNode; }
		| INUM { $$ = new IntNumNode($1); }
		| LEFT_BRACKET expr RIGHT_BRACKET { $$ = $2; }
		;

while	: WHILE expr DO statement { $$ = new WhileNode($2, $4); }
		;

write	: WRITE LEFT_BRACKET expr RIGHT_BRACKET { $$ = new WriteNode($3); }
		;

var		: VAR varlist { $$ = new VarDefNode($2); }
		;	
				
varlist	: ident 
			{
				$$ = new VarDefNode($1 as IdNode); 
			}
		| varlist COMMA ident
			{
				$1.Add($3 as IdNode); 
				$$ = $1; 
			}
		;
%%

