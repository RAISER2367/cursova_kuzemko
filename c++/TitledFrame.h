#ifndef TITLEDFRAME_H
#define TITLEDFRAME_H

#include "UiFrame.h"

class TitledFrame : public UiFrame {
private:
    std::string headerText;
    std::string fontColor;

public:
    TitledFrame();
    TitledFrame(int l, int t, int r, int b, std::string bg, std::string header, std::string fontC);

    // Перевизначення методів
    void updateTheme(std::string newColor) override;
    void printDetails() const override;

    friend std::ostream& operator<<(std::ostream& os, const TitledFrame& tf);
    friend std::istream& operator>>(std::istream& is, TitledFrame& tf);
};

#endif