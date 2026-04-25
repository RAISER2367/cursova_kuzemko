#include "TitledFrame.h"

TitledFrame::TitledFrame() : UiFrame(), headerText("NoName"), fontColor("Black") {}

TitledFrame::TitledFrame(int l, int t, int r, int b, std::string bg, std::string header, std::string fontC)
    : UiFrame(l, t, r, b, bg), headerText(header), fontColor(fontC) {
}

void TitledFrame::updateTheme(std::string newColor) {
    this->fontColor = newColor; // Змінюємо колір тексту замість фону
}

void TitledFrame::printDetails() const {
    UiFrame::printDetails();
    std::cout << " | Шапка: <" << headerText << ">, Колір шрифту: " << fontColor;
}

std::ostream& operator<<(std::ostream& os, const TitledFrame& tf) {
    os << static_cast<const UiFrame&>(tf) << " " << tf.headerText << " " << tf.fontColor;
    return os;
}

std::istream& operator>>(std::istream& is, TitledFrame& tf) {
    is >> static_cast<UiFrame&>(tf) >> tf.headerText >> tf.fontColor;
    return is;
}