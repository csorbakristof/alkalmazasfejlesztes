#include <iostream>
#include <vector>
#include <memory>

using namespace std;

class Blob
{
public:
    Blob(int x, int y)
        : x(x), y(y)
    {
    }

public:
    int x,y;
};

void show(std::weak_ptr<Blob>& blob)
{
    std::shared_ptr<Blob> lockedBlob = blob.lock();

    if (lockedBlob)
    {
        cout << "Blob(" << lockedBlob->x << "," << lockedBlob->y << ")" << endl;
    }
    else
    {
        cout << "Blob does not exist anymore." << endl;
    }
}

int main()
{
    // Creating two containers
    std::vector<std::shared_ptr<Blob>> container0;
    std::vector<std::shared_ptr<Blob>> container1;
    // Adding a blob to both of them
    std::shared_ptr<Blob> newBlob = std::make_shared<Blob>(12,34);
    container0.push_back(newBlob);
    container1.push_back(newBlob);
    // Adding another blob to one of them
    newBlob = std::make_shared<Blob>(56,78);
    container0.push_back(newBlob);

    // Storing a weak pointer to the blob contained in
    //  both containers
    std::weak_ptr<Blob> selectedBlob = (std::shared_ptr<Blob>)container1[0];

    // Successively clearing the containers and accessing the selected blob.
    show(selectedBlob); // Shows Blob(12,34)
    container1.clear();
    show(selectedBlob); // Shows Blob(12,34)
    container0.clear();
    show(selectedBlob); // Blob(12,34) does not exist anymore

    cout << "Finished." << endl;
}
