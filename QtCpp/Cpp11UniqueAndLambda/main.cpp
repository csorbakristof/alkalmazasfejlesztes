#include <iostream>
#include <vector>
#include <memory>
#include <functional>
#include <algorithm>

using namespace std;

// Valami adat, amiket tárolni akarunk.
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

class BlobContainer
{
public:
    void add(std::unique_ptr<Blob>& newBlob)
    {
        // Nem lehet simán másolni, move szemantika kell.
        blobs.push_back(std::move(newBlob));
    }

    // Egy lambda lefuttatása minden elemen
    void ForEach(std::function<void(const Blob&)> lambda ) const
    {
        for(const auto& blob : blobs)
        {
            lambda(*blob);
        }
    }

    auto begin()
    {
        return blobs.begin();
    }

    auto end()
    {
        return blobs.end();
    }

private:
    // Itt tároljuk a blobokat
    std::vector<std::unique_ptr<Blob>> blobs;

};

// Hogy könnyebb legyen a megjelenítés
std::ostream& operator<<(std::ostream& stream, const Blob& blob)
{
    stream << "Blob(" << blob.x << "," << blob.y << ")";
    return stream;
}

int main()
{
    // Kell egy blob tároló
    BlobContainer blobs;

    // Rakunk bele 2 blobot
    std::unique_ptr<Blob> newBlob = std::make_unique<Blob>(12,34);
    blobs.add(newBlob);
    newBlob = std::make_unique<Blob>(56,78);
    blobs.add(newBlob);

    // Az add() után a newBlob weak_ptr már nullptr, mivel
    // a konténer átvette az ownershipet.
    cout << ( newBlob ? "newBlob még érvényes" : "newBlob érvénytelen" ) << endl;

    cout << "Tartalom:" << endl;
    blobs.ForEach( [](const Blob& blob){ cout << blob << endl; } );

    cout << "A tartalom iterátorokkal és std::for_each függvénnyel:" << endl;
    std::for_each(
        blobs.begin(),
        blobs.end(),
        [](std::unique_ptr<Blob>& blob)
            { cout << *blob << endl; });
}
