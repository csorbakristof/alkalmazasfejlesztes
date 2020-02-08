#include <iostream>
#include <vector>
#include <memory>
using namespace std;

/* Ez a példaprogram a Visitor és Observer design patternek működését mutatja be.
 * Egy tároló objektumban (ElementContainer) elemeket (ElementBase leszármazottjai, ElementInt és
 * ElementString) tárolunk.
 *
 * Az observer (Observer osztály, az ObserverBase leszármazottja) minden egyes Elementhez be van regisztrálva,
 * hogy az ha változik a belső állapota (value attribútuma), akkor erről szól az observernek.
 *
 * Egy Visitor (ami a VisitorBase-ből származik) a tárolóban lévő összes elemet módosítja. Tud kezelni
 * ElementInt és ElementString objektumokat is.
 * */

// Elő-deklarációk, hogy a VisitorBase tudjon rájuk hivatkozni.
class ElementInt;
class ElementString;

/* Ősosztály a visitorok számára, melyek ElementInt és ElementString objektumokat tudnak fogadni.
 * A Visitor minta lényege, hogy minden ElementInt és ElementString meg fogja hívni a visitor megfelelő
 * metódusát önmagára, így a visitor minden elemen végre tudja hajtani az általa képviselt műveletet. */
class VisitorBase
{
public:
    virtual void Visit(ElementInt& element) = 0;
    virtual void Visit(ElementString& element) = 0;
};

/* Az observerek ősosztálya. Minden ElementBase leszármazottnak be lehet ilyen
 * objektumokat regisztrálni, hogy ennek szóljanak, ha változik az értékük. */
class ObserverBase
{
public:
    virtual void ValueChanged(int oldValue, int newValue) = 0;
    virtual void ValueChanged(string oldValue, string newValue) = 0;
};

/* Egy konkrét observer implementáció, mely a konzolra kiírja az értékváltozásokat. */
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

/* A tároló elemeinek ősosztálya. Minden elem meg tudja magát jeleníteni, tud kezelni visitort,
 * valamint observereket is be lehet regisztrálni mindegyiknek, amiket értesít, ha az értéke megváltozik. */
class ElementBase
{
protected:
    // Itt tároljuk a pointereket a regisztrált observerekre.
    vector<ObserverBase*> observers;
public:
    virtual void Show() = 0;
    virtual void Accept(VisitorBase& visitor) = 0;

    void RegisterObserver(ObserverBase *observer)
    {
        observers.push_back(observer);
    }
};

/* Egy konkrét elem implementációja, melynek az értéke egy int típusú szám. */
class ElementInt : public ElementBase
{
    int value;
public:
    int GetValue() { return value; }
    void SetValue(int newValue)
    {
        int oldValue = value;
        value = newValue;
        // Minden observer értesítése.
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

    // Visitor fogadása és visszahívása (double dispatch).
    virtual void Accept(VisitorBase& visitor) override
    {
        visitor.Visit(*this);
    }
};

/* Konkrét element, melynek értéke string típusú. */
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

/* Tároló az ElementBase leszármazottjainak. Az elemeknek ő az ownere, a tároló
 * megszűnésekor az elemeit is megszünteti. */
class ElementContainer
{
    // Elemek tárolása
    vector<std::unique_ptr<ElementBase>> elements;
public:
    // Új elem felvétele és az ownership átvétele.
    // A unique_ptr-t csak referenciaként vehetjük át. Érték szerint nem lehet, mert
    //  ahhoz másolni kellene.
    void AddAndTakeOwnership(std::unique_ptr<ElementBase>& element)
    {
        // Itt pedig explicit move szemantikát használunk és nem másolatot készítünk.
        //  Ennek hatására az "element" unique_ptr elveszíti az ownershipet és nullptr lesz.
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

    // Itt lehet egy visitort átadni minden egyes tárolt elemnek.
    void Accept(VisitorBase& visitor)
    {
        for(auto& element : elements)
        {
            element->Accept(visitor);
        }
    }
};

/* Konkrét visitor osztály. Int típus esetén hozzáad 100-at az értékhez,
 * string esetén pedig utána írja, hogy "+100".
 *
 * Ezt a műveletet fogjuk végrehajtani az összes tárolt elemen.
 * (Az ilyen műveletek hatására az Element értesíteni fogja az összes
 * observerét a változásról.) */
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

    // Létrehozunk 4 elemet.
    unique_ptr<ElementBase> newElement = std::make_unique<ElementInt>(1);
    newElement->RegisterObserver(&observer);
    container.AddAndTakeOwnership(newElement);

    // A newElement itt már nullptr mert a container átvette az ownershipet,
    //  így a newElement-et újra lehet hasznosítani.
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
    /* A kimenet:
    --- Before visit:
    ElementInt: 1
    ElementString: 1
    ElementInt: 2
    ElementString: 2
    */

    cout << "--- During visit:" << endl;
    Visitor visitor;
    container.Accept(visitor);
    /* A kimenet:
    --- During visit:
    Int value changed from 1 to 101
    String value changed from 1 to 1+100
    Int value changed from 2 to 102
    String value changed from 2 to 2+100
    */

    cout << "--- After visit:" << endl;
    container.Show();
    /* A kimenet:
    --- After visit:
    ElementInt: 101
    ElementString: 1+100
    ElementInt: 102
    ElementString: 2+100
    */
}
