#-------------------------------------------------
#
# Project created by QtCreator 2018-10-25T14:24:42
#
#-------------------------------------------------

QT       += testlib

QT       -= gui

TARGET = tst_qtestthesimulatortest
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app

INCLUDEPATH += ../SimpleTelemetryVisualizer/Communication \
    ../SimpleTelemetryVisualizer \
    ../SimpleTelemetryVisualizer/Simulator

CONFIG += c++14
QMAKE_CXXFLAGS_CXX11    = -std=c++1y
QT += quick

# The following define makes your compiler emit warnings if you use
# any feature of Qt which as been marked as deprecated (the exact warnings
# depend on your compiler). Please consult the documentation of the
# deprecated API in order to know how to port your code away from it.
DEFINES += QT_DEPRECATED_WARNINGS

# You can also make your code fail to compile if you use deprecated APIs.
# In order to do so, uncomment the following line.
# You can also select to disable deprecated APIs only up to a certain version of Qt.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0


SOURCES += \
        tst_qtestthesimulatortest.cpp \ 
    ../SimpleTelemetryVisualizer/Communication/Communication.cpp \
    ../SimpleTelemetryVisualizer/Simulator/Simulator.cpp \
    ../SimpleTelemetryVisualizer/RobotState.cpp \
    CommunicationMock.cpp

DEFINES += SRCDIR=\\\"$$PWD/\\\"

HEADERS += \
    ../SimpleTelemetryVisualizer/Communication/Communication.h \
    ../SimpleTelemetryVisualizer/Simulator/Simulator.h \
    ../SimpleTelemetryVisualizer/RobotState.h \
    CommunicationMock.h \
    TestSimulator.h
