#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

#define _USE_MATH_DEFINES
#include <math.h>
/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
 
 std::vector<std::string> Split(const std::string& s, char c) {
    std::vector<std::string> res;
    std::string str;
    str = s;
    res.push_back("");
    for (char ch: str) {
        if (ch != c) {
            res.back() += ch;
            continue;
        }
        else {
            res.push_back("");
            continue;
        }
    }
    return res;
}
 
int main()
{
    string LON;
    cin >> LON; cin.ignore();
    string LAT;
    cin >> LAT; cin.ignore();

    double lon = stod(LON);
    double lat = stod(LAT);
    
    double minDist = -1;
    string name = "";
    
    int N;
    cin >> N; cin.ignore();
    for (int i = 0; i < N; i++) {
        string DEFIB;
        
        getline(cin, DEFIB);
        
        std::vector<string> split = Split(DEFIB, ';');
        
        cerr << split[1] << endl;
        
        double defLon = 
            stod(
                split[4].replace(
                    split[4].find(","), 1, "."
                    )
                );
        
        double defLat = 
            stod(
                split[5].replace(
                    split[5].find(","), 1, "."
                    )
                );
        
        double x = (defLon - lon) * cos((lat + defLat)/2),
        y = defLat - lat;
        
        double d = sqrt(
            pow(x, 2) + pow(y, 2)
        ) * 6371;  
        
        if (minDist == -1)
        {
            minDist = d;
            name = split[1];
        }
        else if(d <= minDist)
        {
            minDist = d;
            name = split[1];
        }
    }

    // Write an action using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;

    cout << name << endl;
}
