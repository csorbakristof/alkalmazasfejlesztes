#-------------------------------------------------
#
# Project created by QtCreator 2015-07-13T18:12:03
#
#-------------------------------------------------

CONFIG += c++14
QMAKE_CXXFLAGS_CXX11    = -std=c++1y

QT       += core
QT       += serialport

QT       -= gui

TARGET = QtSocketDemo
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += main.cpp \
    Application.cpp \
    Communication.cpp \
    CommunicationSerialPort.cpp

HEADERS += \
    Application.h \
    Communication.h \
    CommunicationSerialPort.h
