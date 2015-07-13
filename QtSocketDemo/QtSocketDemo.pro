#-------------------------------------------------
#
# Project created by QtCreator 2015-07-13T18:12:03
#
#-------------------------------------------------

CONFIG += c++14
QMAKE_CXXFLAGS_CXX11    = -std=c++1y

QT       += core
QT       += network

QT       -= gui

TARGET = QtSocketDemo
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += main.cpp \
    SocketServer.cpp \
    SocketClient.cpp \
    Application.cpp

HEADERS += \
    SocketServer.h \
    SocketClient.h \
    Application.h
