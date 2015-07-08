#pragma once
#ifndef ROBOTSTATE_H
#define ROBOTSTATE_H
#include <QDataStream>
#include <QString>

/** Represents the full state of the robot in a given time.
 * All visualization can be bound to an instance, or to the history as a list of
 * instances.
 *
 * Also used as a command when sent from the client to the robot.
 */
class RobotState : public QObject
{
    Q_OBJECT

public:
    /**
     * @brief Possible states of the robot
     */
    enum class Status
    {
        /** Default operation */
        Default = 0,
        /** Reset. Used as a command when sent to the robot. */
        Reset = 1,
        /** Instructs the robot to decelerate until stopped.
         * Used both as command and status. */
        Stopping = 2,
        /** Instructs the robot to accelerate.
         * The robot will take the acceleration value from RobotState::a. */
        Accelerate = 3
    };

    /**
     * @brief Constructor
     */
    RobotState();

    /**
     * @brief Constructor with given initial values.
     * @param status    Robot status
     * @param timestamp Timestamp of the state
     * @param x Position
     * @param v Velocity
     * @param a Acceleration
     * @param light Robot light status
     */
    RobotState(Status status, qint64 timestamp,
        float x, float v, float a, qint8 light);

    ~RobotState() = default;

    // We do not implement the NOTIFY signals, just declare them.

    /** Status or command */
    Q_PROPERTY(Status status READ status WRITE setStatus MEMBER _status NOTIFY statusChanged)
    Status status() const { return _status; }
    void setStatus(const Status status) { _status = status; }

    /** Timestamp in ms */
    Q_PROPERTY(float timestamp READ timestamp WRITE setTimestamp MEMBER _timestamp NOTIFY timestampChanged)
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

    /** Status name exposed as property */
    // In QML, it will be accessible as model.statusName
    Q_PROPERTY(QString statusName READ getStatusName NOTIFY statusChanged)

    /** Serializes the object into a stream. */
    void WriteTo(QDataStream& stream) const;

    /** Deserializes the object content from a stream. */
    void ReadFrom(QDataStream& stream);

    /** Copies the values from another instance.
     * Needed due to limitations on copy ctor of QObject instances. */
    void CopyFrom(const RobotState& other);

    /** Returns the name of the status as a human readable string. */
    QString getStatusName() const;

signals:
    /** \addtogroup (Now unused) signals of property changes
     *  @{
     */
    void statusChanged();
    void timestampChanged();
    void xChanged();
    void vChanged();
    void aChanged();
    void lightChanged();
    /** @}*/

private:
    Status _status;
    qint64 _timestamp;
    float _x,_v,_a;
    qint8 _light;

    /** Map of status values and their corresponding, human readable strings.
     * Used by getStatusName(). */
    static std::map<int,QString> statusNames;

    /** Initializes statusNames, called by ctor. */
    void initStatusNames();
};

/** Wraps the RobotState.WriteTo method. */
QDataStream &operator<<(QDataStream &, const RobotState &);

/** Wraps the RobotState.ReadFrom method. */
QDataStream &operator>>(QDataStream &, RobotState &);

#endif // ROBOTSTATE_H
