#-------------------------------------------------
#
# Project created by QtCreator 2016-03-19T14:23:31
#
#-------------------------------------------------

QT       += core

QT       -= gui

CONFIG += c++14

TARGET = StateExample
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += main.cpp \
    Framework/StateStore.cpp

HEADERS += \
    Framework/State.h \
    Framework/Robot.h \
    Framework/StateStore.h \
    States/DefaultState.h \
    States/FastState.h \
    States/EmergencyLineSearchState.h
