#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
int main()
{
    int n; // the number of temperatures to analyse
    cin >> n; cin.ignore();
    
    vector<int> temp;
    
    int res = 0;
    int curDif = 5527;
    
    for (int i = 0; i < n; i++) {
        int t; // a temperature expressed as an integer ranging from -273 to 5526
        cin >> t; cin.ignore();
        temp.push_back(t);
    }
    
    for (int i = 0; i < n; i++) {
        if (temp[i] == 0) {
            cerr << 0 << endl;
            res = 0;
            curDif = 0;
            break;
        }
        else if (temp[i] > 0) {
            cerr << 1 << endl;
            if (temp[i] < curDif) {
                res = temp[i];
                curDif = temp[i];
            }
            else if (temp[i] == curDif && temp[i] == -res) {
                res = temp[i];
            }
        }
        else {
            cerr << -1 << endl;
            if(-temp[i] < curDif) {
                if(res != -temp[i]) {
                    cerr << res << " " << -temp[i] << endl;
                    res = temp[i];
                    curDif = -temp[i];
                }
            }
        }
    }

    // Write an action using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;

    cout << res << endl;
}