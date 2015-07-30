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
    if (lockedBlob) {
        cout << "Blob(" << lockedBlob->x << ","
             << lockedBlob->y << ")" << endl;
    } else {
        cout << "A Blob nem létezik." << endl;
    }
}

int main()
{
    // Két konténer létrehozása
    std::vector<std::shared_ptr<Blob>> container0;
    std::vector<std::shared_ptr<Blob>> container1;
    // Feltöltés
    std::shared_ptr<Blob> newBlob =
            std::make_shared<Blob>(12,34);
    container0.push_back(newBlob);
    container1.push_back(newBlob);
    newBlob = std::make_shared<Blob>(56,78);
    container0.push_back(newBlob);

    // weak_ptr tárolása a mindkét konténerben benne lévő blobra
    std::weak_ptr<Blob> selectedBlob = container1[0];

    // Törlések és a kiválasztott blob vizsgálata
    show(selectedBlob); // Blob(12,34)
    container1.clear();
    show(selectedBlob); // Blob(12,34)
    container0.clear();
    show(selectedBlob); // Már nem létezik.

    cout << "Vége." << endl;
}
