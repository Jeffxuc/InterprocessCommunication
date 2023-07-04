#include <iostream>
#include <string>
#include <stdio.h>
#include <windows.h>
#include <fstream>

using namespace std;

int main(int argc, char* argv[])
{
    printf("argc = %d\n", argc);

    if (argc == 1)
    {
        /*******************************************/
        for (int i = 0; i <= 80; i++)
        {
            fprintf(stderr, "\r%d 4400h: .... .... .... .... %4.2f%%",i, i * 1.25);
            Sleep(15);
        }

        fprintf(stderr, "\nEnd output message");
        cout << endl;
        cout << argv[0] << "; 100" << endl;
        cin.get();

        return 100;
    }
    else
    {
        if (strcmp("/check", argv[1]) == 0)
        {
            //fprintf(stderr, "%4.2f%%", 10.66);

            //Sleep(2000);

            string str_0 = "C:\\ProgramData\\10.txt";
            if (std::ifstream(str_0))
            {
                std::ifstream readFile(str_0);
                if (readFile.is_open())
                {
                    string tmpRes;
                    getline(readFile, tmpRes);
                    cout << "errorCode = " << tmpRes << endl;
                    int errorCode = stoi(tmpRes);


                    switch (errorCode)
                    {
                    case 0:
                        return 0;
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                    case 4:
                        return 4;
                    case 5:
                        return 5;
                    case 6:
                        return 6;
                    case 7:
                        return 7;
                    case 8:
                        return 8;
                    case 9:
                        return 9;
                    case 10:
                        return 10;
                    case 11:
                        return 11;
                    case 12:
                        return 12;
                    case 13:
                        return 13;
                    case 14:
                        return 14;
                    case 15:
                        return 15;
                    default:
                        return 100;
                    }

                }
            }

            return 3;
            
        }
        else if(strcmp("/update", argv[1]) ==0 )
        {
            string str_0 = "C:\\ProgramData\\10.txt";
            int flashRegion = 1;

            if (std::ifstream(str_0))
            {
                std::ifstream readFile(str_0);
                if (readFile.is_open())
                {
                    string tmpVal;
                    while (getline(readFile, tmpVal))
                    {

                    }

                    flashRegion = stoi(tmpVal);

                }
            }

            cout << "\nflashRegion=" << flashRegion << endl;
            cerr << "Updating now....\n";

            int region = 0;
            while (region < flashRegion)
            {
                for (int i = 0; i <= 80; i++)
                {
                    fprintf(stderr, "\r4400h: .... .... .... .... %4.2f%%", i * 1.25);
                    Sleep(20);
                }
                Sleep(500);
                ++region;
            }
            
            string str_1 = "C:\\ProgramData\\20.txt";
            if (std::ifstream(str_1))
                return 0;

            return 39;
            
        }
        else
        {
            cout << argv[1] << "; 3" << endl;
            return 3;
        }
    }
    
}

