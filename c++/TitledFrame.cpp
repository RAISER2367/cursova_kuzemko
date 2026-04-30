#include "TitledFrame.h"

TitledFrame::TitledFrame() : UiFrame(), headerText("NoName"), fontColor("Black") {}

TitledFrame::TitledFrame(int l, int t, int r, int b, std::string bg, std::string header, std::string fontC)
    : UiFrame(l, t, r, b, bg), headerText(header), fontColor(fontC) {
}

TitledFrame::TitledFrame(const TitledFrame& other)
    : UiFrame(other), headerText(other.headerText), fontColor(other.fontColor) {
}

TitledFrame::TitledFrame(TitledFrame&& other) noexcept
    : UiFrame(std::move(other)), headerText(std::move(other.headerText)), fontColor(std::move(other.fontColor)) {
}

TitledFrame& TitledFrame::operator=(const TitledFrame& other) {
    if (this != &other) {
        UiFrame::operator=(other);
        headerText = other.headerText;
        fontColor = other.fontColor;
    }
    return *this;
}

TitledFrame& TitledFrame::operator=(TitledFrame&& other) noexcept {
    if (this != &other) {
        UiFrame::operator=(std::move(other));
        headerText = std::move(other.headerText);
        fontColor = std::move(other.fontColor);
    }
    return *this;
}

void TitledFrame::updateTheme(std::string newColor) {
    this->fontColor = newColor;
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