using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        string rom1 = Console.ReadLine();
        string rom2 = Console.ReadLine();
        int i1 = 0, i2 = 0;
        i1 = RomToInt(rom1);
        i2 = RomToInt(rom2);
        
        int res = i1+i2;
        string result = IntToRom(res);
        Console.WriteLine(result);
    }
    
    static int RomToInt(string rom1)
    {
        int i1 = 0;
        for(int i = rom1.Length - 1; i >= 0; i--)
        {
            switch (rom1[i])
            {
                case 'I':
                {
                    if(i < rom1.Length - 1)
                    {
                        if(rom1[i+1] == 'V' || rom1[i+1] == 'X')
                        {
                            i1 -= 1;
                            break;
                        }
                    }
                    i1+=1;
                }
                break;
                case 'V':
                {
                    i1+=5;
                }
                break;
                case 'X':
                {
                    if(i < rom1.Length - 1)
                    {
                        if(rom1[i+1] == 'L' || rom1[i+1] == 'C')
                        {
                            i1 -= 10;
                            break;
                        }
                    }
                    i1+=10;
                }
                break;
                case 'L':
                {
                    i1+=50;
                }
                break;
                case 'C':
                {
                    if(i < rom1.Length - 1)
                    {
                        if(rom1[i+1] == 'D' || rom1[i+1] == 'M')
                        {
                            i1 -= 100;
                            break;
                        }
                    }
                    i1+=100;
                }
                break;
                case 'D':
                {
                    i1+=500;
                }
                break;
                case 'M':
                {
                    i1+=1000;
                }
                break;
            }
        }
        return i1;
    }
    
    static string IntToRom(int num)
    {
        string res = "";
        string s = num.ToString();
        switch(s[s.Length-1])
        {
            case '1':
            res = 'I' + res;
            break;
            case '2':
            res = "II" + res;
            break;
            case '3':
            res = "III" + res;
            break;
            case '4':
            res = "IV" + res;
            break;
            case '5':
            res = 'V' + res;
            break;
            case '6':
            res = "VI" + res;
            break;
            case '7':
            res = "VII" + res;
            break;
            case '8':
            res = "VIII" + res;
            break;
            case '9':
            res = "IX" + res;
            break;
        }
        if(num >= 10)
        {
            switch(s[s.Length-2])
            {
                case '1':
            res = 'X' + res;
            break;
            case '2':
            res = "XX" + res;
            break;
            case '3':
            res = "XXX" + res;
            break;
            case '4':
            res = "XL" + res;
            break;
            case '5':
            res = 'L' + res;
            break;
            case '6':
            res = "LX" + res;
            break;
            case '7':
            res = "LXX" + res;
            break;
            case '8':
            res = "LXXX" + res;
            break;
            case '9':
            res = "XC" + res;
            break;
            }
            if(num >= 100)
            {
                switch(s[s.Length-3])
                {
                    case '1':
                    res = 'C' + res;
                    break;
                    case '2':
                    res = "CC" + res;
                    break;
                    case '3':
                    res = "CCC" + res;
                    break;
                    case '4':
                    res = "CD" + res;
                    break;
                    case '5':
                    res = 'D' + res;
                    break;
                    case '6':
                    res = "DC" + res;
                    break;
                    case '7':
                    res = "DCC" + res;
                    break;
                    case '8':
                    res = "DCCC" + res;
                    break;
                    case '9':
                    res = "CM" + res;
                    break;
                }
                
                if(num >= 1000)
                {
                    switch(s[s.Length-4])
                    {
                        case '1':
                        res = 'M' + res;
                        break;
                        case '2':
                        res = "MM" + res;
                        break;
                        case '3':
                        res = "MMM" + res;
                        break;
                        case '4':
                        res = "MMMM" + res;
                        break;
                        default:
                        break;
                    }
                }
                else
                {
                    return res;
                }
            }
            else
            {
                return res;
            }
        }
        else
        {
            return res;
        }
        return res;
    }
}