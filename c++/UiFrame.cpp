#include "UiFrame.h"

UiFrame::UiFrame(int l, int t, int r, int b, std::string color)
    : left(l), top(t), right(r), bottom(b), fillColor(color) {
}

void UiFrame::updateTheme(std::string newColor) {
    this->fillColor = newColor;
}

void UiFrame::printDetails() const {
    std::cout << "Фрейм: [" << left << ":" << top << " -> " << right << ":" << bottom << "], Заливка: " << fillColor;
}

UiFrame::UiFrame(const UiFrame& other)
    : left(other.left), top(other.top), right(other.right), bottom(other.bottom), fillColor(other.fillColor) {
}

UiFrame::UiFrame(UiFrame&& other) noexcept
    : left(other.left), top(other.top), right(other.right), bottom(other.bottom), fillColor(std::move(other.fillColor)) {
    other.left = 0; other.top = 0; other.right = 0; other.bottom = 0;
}

UiFrame UiFrame::operator+(const UiFrame& other) const {
    return UiFrame(std::min(left, other.left), std::min(top, other.top),
        std::max(right, other.right), std::max(bottom, other.bottom), fillColor);
}

void operator+=(UiFrame& frame, int offset) {
    frame.left += offset; frame.top += offset; frame.right += offset; frame.bottom += offset;
}

UiFrame& UiFrame::operator=(const UiFrame& other) {
    if (this != &other) {
        left = other.left; top = other.top; right = other.right; bottom = other.bottom;
        fillColor = other.fillColor;
    }
    return *this;
}

UiFrame& UiFrame::operator=(UiFrame&& other) noexcept {
    if (this != &other) {
        left = other.left; top = other.top; right = other.right; bottom = other.bottom;
        fillColor = std::move(other.fillColor);
        other.left = 0; other.top = 0; other.right = 0; other.bottom = 0;
    }
    return *this;
}

std::ostream& operator<<(std::ostream& os, const UiFrame& frame) {
    os << frame.left << " " << frame.top << " " << frame.right << " " << frame.bottom << " " << frame.fillColor;
    return os;
}

std::istream& operator>>(std::istream& is, UiFrame& frame) {
    is >> frame.left >> frame.top >> frame.right >> frame.bottom >> frame.fillColor;
    return is;
}