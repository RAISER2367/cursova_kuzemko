#ifndef UIFRAME_H
#define UIFRAME_H

#include <iostream>
#include <string>
#include <algorithm>

class UiFrame {
protected:
    int left, top, right, bottom;
    std::string fillColor;

public:
    UiFrame(int l = 0, int t = 0, int r = 100, int b = 100, std::string color = "White");
    virtual ~UiFrame() {}

    // Валідація координат
    static bool checkBounds(int l, int t, int r, int b) { return (r > l && b > t); }

    // Віртуальні методи для поліморфізму
    virtual void updateTheme(std::string newColor);
    virtual void printDetails() const;

    // Перевантаження операторів
    UiFrame operator+(const UiFrame& other) const;
    friend void operator+=(UiFrame& frame, int offset);
    UiFrame& operator=(const UiFrame& other);

    // Конструктор копіювання та переміщення
    UiFrame(const UiFrame& other);
    UiFrame(UiFrame&& other) noexcept;

    // Робота з потоками
    friend std::ostream& operator<<(std::ostream& os, const UiFrame& frame);
    friend std::istream& operator>>(std::istream& is, UiFrame& frame);
};

#endif