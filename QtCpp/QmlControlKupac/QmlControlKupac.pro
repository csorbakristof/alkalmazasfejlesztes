CONFIG += c++14
QMAKE_CXXFLAGS_CXX11    = -std=c++1y

TEMPLATE = app

QT += qml quick widgets

SOURCES += main.cpp \
    MainWindowCppSide.cpp

RESOURCES += qml.qrc

# Additional import path used to resolve QML modules in Qt Creator's code model
QML_IMPORT_PATH =

# Default rules for deployment.
include(deployment.pri)

HEADERS += \
    MainWindowCppSide.h
