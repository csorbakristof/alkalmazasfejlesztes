#pragma once
#ifndef ROBOTSTATE_H
#define ROBOTSTATE_H
#include "Communication/Parcel.h"
#include <QDataStream>
#include <QString>

/** Represents the full state of the robot in a given time.
 * All visualization can be bound to an instance, or to the history as a list of
 * instances. */
class RobotState : public QObject, public Parcel
{
    Q_OBJECT

public:
    enum class Status
    {
        Default = 0,
        Reset = 1,
        Stopping = 2,
        Accelerate = 3    // Command, copies RobotStatus.a
    };

    RobotState();

    RobotState(Status status, qint64 timestamp,
        float x, float v, float a, qint8 light);

    ~RobotState() = default;

    /** Status or command */
    Q_PROPERTY(Status status READ status WRITE setStatus MEMBER _status NOTIFY statusChanged)
    Status status() const { return _status; }
    void setStatus(const Status status) { _status = status; }

    /** Timestamp in ms */
    Q_PROPERTY(qint64 timestamp READ timestamp WRITE setTimestamp MEMBER _timestamp NOTIFY timestampChanged)
    qint64 timestamp() const { return _timestamp; }
    void setTimestamp(const qint64 timestamp) { _timestamp = timestamp; }

    /** Position in meter */
    Q_PROPERTY(float x READ x WRITE setX MEMBER _x NOTIFY xChanged)
    float x() const { return _x; }
    void setX(float x) { _x = x; }

    /** Velocity in m/s */
    Q_PROPERTY(float v READ v WRITE setV MEMBER _v NOTIFY vChanged)
    float v() const { return _v; }
    void setV(float v) { _v = v; }

    /** Acceleration in m/s2 */
    Q_PROPERTY(float a READ a WRITE setA MEMBER _a NOTIFY aChanged)
    float a() const { return _a; }
    void setA(float a) { _a = a; }

    /** Status information of the headlights of the robot. */
    Q_PROPERTY(bool light READ light WRITE setLight MEMBER _light NOTIFY lightChanged)
    float light() const { return _light; }
    void setLight(float light) { _light = light; }

    virtual void WriteTo(QDataStream& stream) const override;
    void ReadFrom(QDataStream& stream);
    void CopyFrom(const RobotState& other);

    QString getStatusName() const;

signals:
    void statusChanged();
    void timestampChanged();
    void xChanged();
    void vChanged();
    void aChanged();
    void lightChanged();


private:
    Status _status;
    qint64 _timestamp;
    float _x,_v,_a;
    qint8 _light;

    static std::map<int,QString> statusNames;
    void initStatusNames();
};

#endif // ROBOTSTATE_H
