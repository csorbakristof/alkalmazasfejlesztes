#-------------------------------------------------
#
# Project created by QtCreator 2015-07-30T12:08:17
#
#-------------------------------------------------
CONFIG += c++14
QMAKE_CXXFLAGS_CXX11    = -std=c++1y

QT       += core

QT       -= gui

TARGET = AproPeldak
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += main.cpp \
    SignalPelda.cpp

HEADERS += \
    SignalPelda.h
