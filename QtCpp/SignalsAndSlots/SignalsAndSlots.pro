#-------------------------------------------------
#
# Project created by QtCreator 2015-07-18T11:15:26
#
#-------------------------------------------------

CONFIG += c++14
QMAKE_CXXFLAGS_CXX11    = -std=c++1y

QT       += core

QT       -= gui

TARGET = SignalsAndSlots
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += main.cpp \
    Simulator.cpp \
    Application.cpp

HEADERS += \
    Simulator.h \
    Application.h
