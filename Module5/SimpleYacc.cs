// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.0
// Machine:  DESKTOP-LOVCSUB
// DateTime: 18.12.2019 13:48:12
// UserName: Анна
// Input file <SimpleYacc.y - 18.12.2019 13:48:06>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace SimpleParser
{
public enum Tokens {
    error=1,EOF=2,BEGIN=3,END=4,CYCLE=5,INUM=6,
    RNUM=7,ID=8,ASSIGN=9,SEMICOLON=10,WHILE=11,DO=12,
    REPEAT=13,UNTIL=14,FOR=15,TO=16,IF=17,THEN=18,
    ELSE=19,VAR=20,COMMA=21,WRITE=22,PLUS=23,MINUS=24,
    MULTIPLICATION=25,DIVISION=26,LEFT_BRACKET=27,RIGHT_BRACKET=28};

// Abstract base class for GPLEX scanners
public abstract class ScanBase : AbstractScanner<int,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
public class ScanObj {
  public int token;
  public int yylval;
  public LexLocation yylloc;
  public ScanObj( int t, int val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

public class Parser: ShiftReduceParser<int, LexLocation>
{
  // Verbatim content from SimpleYacc.y - 18.12.2019 13:48:06
// Ýòè îáúÿâëåíèÿ äîáàâëÿþòñÿ â êëàññ GPPGParser, ïðåäñòàâëÿþùèé ñîáîé ïàðñåð, ãåíåðèðóåìûé ñèñòåìîé gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
  // End verbatim content from SimpleYacc.y - 18.12.2019 13:48:06

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[37];
  private static State[] states = new State[71];
  private static string[] nonTerms = new string[] {
      "progr", "$accept", "block", "stlist", "statement", "assign", "cycle", 
      "while", "repeat", "for", "write", "if", "idents", "ident", "expr", "T", 
      "F", };

  static Parser() {
    states[0] = new State(new int[]{3,4},new int[]{-1,1,-3,3});
    states[1] = new State(new int[]{2,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-4,5,-5,46,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[5] = new State(new int[]{4,6,10,7});
    states[6] = new State(-18);
    states[7] = new State(new int[]{8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-5,8,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[8] = new State(-4);
    states[9] = new State(-5);
    states[10] = new State(new int[]{9,11});
    states[11] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,12,-14,28,-16,30,-17,27});
    states[12] = new State(new int[]{23,13,24,23,4,-15,10,-15,14,-15,19,-15,16,-15});
    states[13] = new State(new int[]{8,18,6,19,27,20},new int[]{-16,14,-17,27,-14,17});
    states[14] = new State(new int[]{25,15,26,25,23,-29,24,-29,4,-29,10,-29,14,-29,19,-29,16,-29,28,-29,8,-29,3,-29,5,-29,11,-29,13,-29,15,-29,22,-29,17,-29,20,-29,12,-29,18,-29});
    states[15] = new State(new int[]{8,18,6,19,27,20},new int[]{-17,16,-14,17});
    states[16] = new State(-32);
    states[17] = new State(-34);
    states[18] = new State(-14);
    states[19] = new State(-35);
    states[20] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,21,-14,28,-16,30,-17,27});
    states[21] = new State(new int[]{28,22,23,13,24,23});
    states[22] = new State(-36);
    states[23] = new State(new int[]{8,18,6,19,27,20},new int[]{-16,24,-17,27,-14,17});
    states[24] = new State(new int[]{25,15,26,25,23,-30,24,-30,4,-30,10,-30,14,-30,19,-30,16,-30,28,-30,8,-30,3,-30,5,-30,11,-30,13,-30,15,-30,22,-30,17,-30,20,-30,12,-30,18,-30});
    states[25] = new State(new int[]{8,18,6,19,27,20},new int[]{-17,26,-14,17});
    states[26] = new State(-33);
    states[27] = new State(-31);
    states[28] = new State(new int[]{23,-16,24,-16,4,-16,10,-16,14,-16,19,-16,16,-16,28,-16,8,-16,3,-16,5,-16,11,-16,13,-16,15,-16,22,-16,17,-16,20,-16,12,-16,18,-16,25,-34,26,-34});
    states[29] = new State(new int[]{23,-17,24,-17,4,-17,10,-17,14,-17,19,-17,16,-17,28,-17,8,-17,3,-17,5,-17,11,-17,13,-17,15,-17,22,-17,17,-17,20,-17,12,-17,18,-17,25,-35,26,-35});
    states[30] = new State(new int[]{25,15,26,25,23,-28,24,-28,4,-28,10,-28,14,-28,19,-28,16,-28,28,-28,8,-28,3,-28,5,-28,11,-28,13,-28,15,-28,22,-28,17,-28,20,-28,12,-28,18,-28});
    states[31] = new State(-6);
    states[32] = new State(-7);
    states[33] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,34,-14,28,-16,30,-17,27});
    states[34] = new State(new int[]{23,13,24,23,8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-5,35,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[35] = new State(-19);
    states[36] = new State(-8);
    states[37] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,38,-14,28,-16,30,-17,27});
    states[38] = new State(new int[]{12,39,23,13,24,23});
    states[39] = new State(new int[]{8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-5,40,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[40] = new State(-20);
    states[41] = new State(-9);
    states[42] = new State(new int[]{8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-4,43,-5,46,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[43] = new State(new int[]{14,44,10,7});
    states[44] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,45,-14,28,-16,30,-17,27});
    states[45] = new State(new int[]{23,13,24,23,4,-21,10,-21,14,-21,19,-21});
    states[46] = new State(-3);
    states[47] = new State(-10);
    states[48] = new State(new int[]{8,18},new int[]{-6,49,-14,10});
    states[49] = new State(new int[]{16,50});
    states[50] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,51,-14,28,-16,30,-17,27});
    states[51] = new State(new int[]{12,52,23,13,24,23});
    states[52] = new State(new int[]{8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-5,53,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[53] = new State(-22);
    states[54] = new State(-11);
    states[55] = new State(new int[]{27,56});
    states[56] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,57,-14,28,-16,30,-17,27});
    states[57] = new State(new int[]{28,58,23,13,24,23});
    states[58] = new State(-23);
    states[59] = new State(-12);
    states[60] = new State(new int[]{8,18,6,29,27,20},new int[]{-15,61,-14,28,-16,30,-17,27});
    states[61] = new State(new int[]{18,62,23,13,24,23});
    states[62] = new State(new int[]{8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-5,63,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[63] = new State(new int[]{19,64,4,-24,10,-24,14,-24});
    states[64] = new State(new int[]{8,18,3,4,5,33,11,37,13,42,15,48,22,55,17,60,20,69},new int[]{-5,65,-6,9,-14,10,-3,31,-7,32,-8,36,-9,41,-10,47,-11,54,-12,59,-13,66});
    states[65] = new State(-25);
    states[66] = new State(new int[]{21,67,4,-13,10,-13,14,-13,19,-13});
    states[67] = new State(new int[]{8,18},new int[]{-14,68});
    states[68] = new State(-27);
    states[69] = new State(new int[]{8,18},new int[]{-14,70});
    states[70] = new State(-26);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,2});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-4, new int[]{-5});
    rules[4] = new Rule(-4, new int[]{-4,10,-5});
    rules[5] = new Rule(-5, new int[]{-6});
    rules[6] = new Rule(-5, new int[]{-3});
    rules[7] = new Rule(-5, new int[]{-7});
    rules[8] = new Rule(-5, new int[]{-8});
    rules[9] = new Rule(-5, new int[]{-9});
    rules[10] = new Rule(-5, new int[]{-10});
    rules[11] = new Rule(-5, new int[]{-11});
    rules[12] = new Rule(-5, new int[]{-12});
    rules[13] = new Rule(-5, new int[]{-13});
    rules[14] = new Rule(-14, new int[]{8});
    rules[15] = new Rule(-6, new int[]{-14,9,-15});
    rules[16] = new Rule(-15, new int[]{-14});
    rules[17] = new Rule(-15, new int[]{6});
    rules[18] = new Rule(-3, new int[]{3,-4,4});
    rules[19] = new Rule(-7, new int[]{5,-15,-5});
    rules[20] = new Rule(-8, new int[]{11,-15,12,-5});
    rules[21] = new Rule(-9, new int[]{13,-4,14,-15});
    rules[22] = new Rule(-10, new int[]{15,-6,16,-15,12,-5});
    rules[23] = new Rule(-11, new int[]{22,27,-15,28});
    rules[24] = new Rule(-12, new int[]{17,-15,18,-5});
    rules[25] = new Rule(-12, new int[]{17,-15,18,-5,19,-5});
    rules[26] = new Rule(-13, new int[]{20,-14});
    rules[27] = new Rule(-13, new int[]{-13,21,-14});
    rules[28] = new Rule(-15, new int[]{-16});
    rules[29] = new Rule(-15, new int[]{-15,23,-16});
    rules[30] = new Rule(-15, new int[]{-15,24,-16});
    rules[31] = new Rule(-16, new int[]{-17});
    rules[32] = new Rule(-16, new int[]{-16,25,-17});
    rules[33] = new Rule(-16, new int[]{-16,26,-17});
    rules[34] = new Rule(-17, new int[]{-14});
    rules[35] = new Rule(-17, new int[]{6});
    rules[36] = new Rule(-17, new int[]{27,-15,28});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

}
}
