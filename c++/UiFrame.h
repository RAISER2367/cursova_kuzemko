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

    static bool checkBounds(int l, int t, int r, int b) { return (r > l && b > t); }

    // Гетери та Сетери
    int getLeft() const { return left; }
    void setLeft(int l) { left = l; }
    int getTop() const { return top; }
    void setTop(int t) { top = t; }
    int getRight() const { return right; }
    void setRight(int r) { right = r; }
    int getBottom() const { return bottom; }
    void setBottom(int b) { bottom = b; }
    std::string getFillColor() const { return fillColor; }
    void setFillColor(const std::string& c) { fillColor = c; }

    virtual void updateTheme(std::string newColor);
    virtual void printDetails() const;

    UiFrame operator+(const UiFrame& other) const;
    friend void operator+=(UiFrame& frame, int offset);

    UiFrame& operator=(const UiFrame& other);
    UiFrame& operator=(UiFrame&& other) noexcept; // Оператор переміщення

    UiFrame(const UiFrame& other);
    UiFrame(UiFrame&& other) noexcept;

    friend std::ostream& operator<<(std::ostream& os, const UiFrame& frame);
    friend std::istream& operator>>(std::istream& is, UiFrame& frame);
};

#endif