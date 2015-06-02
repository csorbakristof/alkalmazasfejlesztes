#include <iostream>
#include <vector>
#include <memory>
#include <functional>

using namespace std;

// Some kind of blob we need to store.
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

// Stores blobs
class BlobContainer
{
public:
    void add(std::unique_ptr<Blob>& newBlob)
    {
        // Cannot simply copy, we need to move.
        blobs.push_back(std::move(newBlob));
    }

    // Execute a lambda on all stored elements
    void ForEach(std::function<void(const Blob&)> lambda )
    {
        for(const auto& blob : blobs)
        {
            lambda(*blob);
        }
    }

private:
    // This is where we store the blobs
    std::vector<std::unique_ptr<Blob>> blobs;

};

// To make the output easier
std::ostream& operator<<(std::ostream& stream, const Blob& blob)
{
    stream << "Blob(" << blob.x << "," << blob.y << ")";
    return stream;
}

int main()
{
    // Create a set of blobs (which takes ownership)
    BlobContainer blobs;

    // Add 2 blobs
    std::unique_ptr<Blob> newBlob = std::make_unique<Blob>(12,34);
    blobs.add(newBlob);
    newBlob = std::make_unique<Blob>(56,78);
    blobs.add(newBlob);

    // After adding, the newBlob pointer is null, as the ownership has moved
    //  into the container.
    cout << ( newBlob ? "newBlob is still valid" : "newBlob is invalid" ) << endl;

    cout << "Contents:" << endl;
    blobs.ForEach( [](const Blob& blob){ cout << blob << endl; } );
}
