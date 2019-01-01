#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

#include <list>

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
 
 bool compare (const int& first, const int& second)
{
  return ( first < second );
}
 
int main()
{
    int N;
    
    std::list<int> List;
    
    cin >> N; cin.ignore();
    for (int i = 0; i < N; i++) {
        int Pi;
        cin >> Pi; cin.ignore();
        List.push_back(Pi);
    }
    
    List.sort(compare);
    
    int dif = List.back();
    int last = 0;
    for (int i = 0; i<N-1; i++) {
        int n = List.front();
        List.pop_front();
        last = List.front();
        int d = last - n;
        if (d<dif && d >= 0) {
            dif = d;
        }
    }

    // Write an action using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;

    cout << dif << endl;
}