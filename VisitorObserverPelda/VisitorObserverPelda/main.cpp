#include <iostream>
#include <vector>
#include <memory>

using namespace std;

class ElementInt;
class ElementString;

class VisitorBase
{
public:
    virtual void Visit(ElementInt& element) = 0;
    virtual void Visit(ElementString& element) = 0;
};

class ObserverBase
{
public:
    virtual void ValueChanged(int oldValue, int newValue) = 0;
    virtual void ValueChanged(string oldValue, string newValue) = 0;
};

class Observer : public ObserverBase
{
public:
    virtual void ValueChanged(int oldValue, int newValue) override
    {
        cout << "Int value changed from " << oldValue << " to " << newValue << endl;
    }

    virtual void ValueChanged(string oldValue, string newValue) override
    {
        cout << "String value changed from " << oldValue << " to " << newValue << endl;
    }
};


class ElementBase
{
protected:
    vector<ObserverBase*> observers;
public:
    virtual void Show() = 0;
    virtual void Accept(VisitorBase& visitor) = 0;
    void RegisterObserver(ObserverBase *observer)
    {
        observers.push_back(observer);
    }
};

class ElementInt : public ElementBase
{
    int value;
public:
    int GetValue() { return value; }
    void SetValue(int newValue)
    {
        int oldValue = value;
        value = newValue;
        for(auto observer : observers)
        {
            observer->ValueChanged(oldValue, newValue);
        }
    }

    ElementInt(const int value = 0)
        : value(value)
    {
    }

    virtual void Show() override
    {
        cout << "ElementInt: " << value << endl;
    }

    virtual void Accept(VisitorBase& visitor) override
    {
        visitor.Visit(*this);
    }
};

class ElementString : public ElementBase
{
    string value;
public:
    string GetValue() { return value; }
    void SetValue(string newValue)
    {
        string oldValue = value;
        value = newValue;
        for(auto observer : observers)
        {
            observer->ValueChanged(oldValue, newValue);
        }
    }

    ElementString(const string value)
        : value(value)
    {
    }

    virtual void Show() override
    {
        cout << "ElementString: " << value << endl;
    }

    virtual void Accept(VisitorBase& visitor) override
    {
        visitor.Visit(*this);
    }
};

class ElementContainer
{
    vector<std::unique_ptr<ElementBase>> elements;
public:
    void AddAndTakeOwnership(std::unique_ptr<ElementBase>& element)
    {
        elements.push_back(std::move(element));
    }

    void Show()
    {
        // Csak referencia szerint interalhatunk, mivel a unique_ptr masolasa tilos.
        for(auto& element : elements)
        {
            element->Show();
        }
    }

    void Accept(VisitorBase& visitor)
    {
        for(auto& element : elements)
        {
            element->Accept(visitor);
        }
    }
};

class Visitor : public VisitorBase
{
public:
    virtual void Visit(ElementInt& element) override
    {
        element.SetValue(element.GetValue() + 100);
    }

    virtual void Visit(ElementString& element) override
    {
        element.SetValue(element.GetValue() + "+100");
    }
};

int main()
{
    ElementContainer container;
    Observer observer;

    unique_ptr<ElementBase> newElement = std::make_unique<ElementInt>(1);
    newElement->RegisterObserver(&observer);
    container.AddAndTakeOwnership(newElement);

    newElement = std::make_unique<ElementString>("1");
    newElement->RegisterObserver(&observer);
    container.AddAndTakeOwnership(newElement);

    newElement = std::make_unique<ElementInt>(2);
    newElement->RegisterObserver(&observer);
    container.AddAndTakeOwnership(newElement);

    newElement = std::make_unique<ElementString>("2");
    newElement->RegisterObserver(&observer);
    container.AddAndTakeOwnership(newElement);

    cout << "--- Before visit:" << endl;
    container.Show();

    cout << "--- During visit:" << endl;
    Visitor visitor;
    container.Accept(visitor);

    cout << "--- After visit:" << endl;
    container.Show();
}

