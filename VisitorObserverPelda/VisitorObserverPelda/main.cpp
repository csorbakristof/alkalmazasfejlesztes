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

class ElementBase
{
public:
    virtual void Show() = 0;
    virtual void Accept(VisitorBase& visitor) = 0;
};

class ElementInt : public ElementBase
{
public:
    // A kod tomorsege erdekeben ez most public.
    int value;

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
public:
    // A kod tomorsege erdekeben ez most public.
    string value;

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
    void AddAndTakeOwnership(ElementBase *element)
    {
        elements.push_back(unique_ptr<ElementBase>(element));
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
        element.value += 100;
    }

    virtual void Visit(ElementString& element) override
    {
        element.value = element.value + "+100";
    }
};

int main()
{
    ElementContainer container;
    container.AddAndTakeOwnership(new ElementInt(1));
    container.AddAndTakeOwnership(new ElementString("1"));
    container.AddAndTakeOwnership(new ElementInt(2));
    container.AddAndTakeOwnership(new ElementString("2"));

    cout << "--- Before visit:" << endl;
    container.Show();

    Visitor visitor;
    container.Accept(visitor);

    cout << "--- After visit:" << endl;
    container.Show();
}

