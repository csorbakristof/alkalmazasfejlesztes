TEMPLATE = app

CONFIG += c++14
QMAKE_CXXFLAGS_CXX11    = -std=c++1y

QT += qml quick widgets
QT += serialport

SOURCES += main.cpp \
    MainWindowsEventHandling.cpp \
    RobotProxy.cpp \
    RobotState.cpp \
    RobotStateHistory.cpp \
    Communication/Communication.cpp \
    Communication/CommunicationTcpSocket.cpp \
    Communication/CommunicationTcpSocketServer.cpp \
    Communication/CommunicationTcpSocketClient.cpp \
    Simulator/Simulator.cpp \
    Communication/CommunicationSerialPort.cpp \
    StvApplication.cpp

RESOURCES += qml.qrc

# Additional import path used to resolve QML modules in Qt Creator's code model
#QML_IMPORT_PATH =

# Default rules for deployment.
include(deployment.pri)

HEADERS += \
    MainWindowsEventHandling.h \
    RobotProxy.h \
    RobotState.h \
    RobotStateHistory.h \
    Communication/Communication.h \
    Communication/CommunicationTcpSocket.h \
    Communication/CommunicationTcpSocketServer.h \
    Communication/CommunicationTcpSocketClient.h \
    Simulator/Simulator.h \
    Communication/CommunicationSerialPort.h \
    StvApplication.h
