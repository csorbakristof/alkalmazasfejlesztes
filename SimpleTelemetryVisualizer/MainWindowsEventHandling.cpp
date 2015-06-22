#include "MainWindowsEventHandling.h"
#include "RobotProxy.h"

MainWindowsEventHandling::MainWindowsEventHandling(RobotProxy &robot)
    : robot(robot)
{

}

void MainWindowsEventHandling::accelerateCommand()
{
    robot.accelerate();
}

void MainWindowsEventHandling::stopCommand()
{
    robot.stop();
}

void MainWindowsEventHandling::resetCommand()
{
    robot.reset();
}
