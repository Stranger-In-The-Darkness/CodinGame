#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
 bool Reverse(string from, string to) 
 {
    std::string str = "";
    int i = 0;
    while (true)
    {
        int count = 1;
        for (int j = i; j < from.length()-1; j++)
        {
            if (from[j] == from[j+1]) 
            {
                count++;
            }
            else
            {
                break;
            }
        }
        str += std::to_string(count) + from[i];
        i = i + count;
        if (i >= from.length()) { break; }
    }
    cerr << "str = " << str << " from = " << from << " to = " << to << endl;
    if (str != to) { return false; }
    return true;
}
 
int main()
{
    std::string s;
    getline(cin, s);
    
    std::string res = s;
    
    while (res.length() % 2 == 0)
    {
        std::string r = "";
        int count = 0;
        for(int i = 0; i < res.length(); i+=2){
            count = res[i] - '0';
            r += std::string(count, res[i+1]);
        }
        cerr << r << " " << res << endl;
        if (res == r) { break; }
        if (Reverse(r, res)) {
            res = r;
        }
        else { break; }
    }
    cout << res;   
}