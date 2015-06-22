#pragma once
#ifndef PARCEL_H
#define PARCEL_H

class QDataStream;

/** Represents a data structure which can be written into
 * or read from a data stream. */
class Parcel
{
public:
    virtual void WriteTo(QDataStream& stream) const = 0;
};

#endif // PARCEL_H
