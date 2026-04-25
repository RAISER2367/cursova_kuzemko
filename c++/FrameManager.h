#ifndef FRAMEMANAGER_H
#define FRAMEMANAGER_H

#include "TitledFrame.h"
#include <vector>
#include <fstream>

class FrameIterator {
    TitledFrame* current;
public:
    FrameIterator(TitledFrame* ptr) : current(ptr) {}
    TitledFrame& operator*() { return *current; }
    FrameIterator& operator++() { current++; return *this; }
    bool operator!=(const FrameIterator& other) const { return current != other.current; }
};

class FrameManager {
private:
    std::vector<TitledFrame> frameCollection;

public:
    void addFrame(const TitledFrame& frame) { frameCollection.insert(frameCollection.begin(), frame); }
    void removeFrame(int index);
    void bringToFront(int index);

    void showAllFrames() const;
    void exportData(std::string filename) const;
    void importData(std::string filename);

    FrameIterator begin() { return FrameIterator(frameCollection.empty() ? nullptr : &frameCollection[0]); }
    FrameIterator end() { return FrameIterator(frameCollection.empty() ? nullptr : &frameCollection[0] + frameCollection.size()); }
    size_t getSize() const { return frameCollection.size(); }
};

#endif