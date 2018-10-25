#ifndef COMMUNICATIONMOCK_H
#define COMMUNICATIONMOCK_H
#include "Communication.h"

class CommunicationMock : public Communication
{
public:
    CommunicationMock();

    bool DidCommunicate = false;

    virtual bool isConnected() const override
    {
        return true;
    }

    virtual void sendBufferContent()
    {
        DidCommunicate = true;
    }
};

#endif // COMMUNICATIONMOCK_H
