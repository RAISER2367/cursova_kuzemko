#include "FrameManager.h"

void FrameManager::removeFrame(int index) {
    if (index >= 0 && index < (int)frameCollection.size()) {
        frameCollection.erase(frameCollection.begin() + index);
    }
}

void FrameManager::bringToFront(int index) {
    if (index > 0 && index < (int)frameCollection.size()) {
        TitledFrame selected = frameCollection[index];
        frameCollection.erase(frameCollection.begin() + index);
        frameCollection.insert(frameCollection.begin(), selected);
    }
}

void FrameManager::showAllFrames() const {
    for (size_t i = 0; i < frameCollection.size(); ++i) {
        std::cout << "ID[" << i << "] -> ";
        // Демонстрація поліморфізму
        const UiFrame* basePtr = &frameCollection[i];
        basePtr->printDetails();
        std::cout << std::endl;
    }
}

void FrameManager::exportData(std::string filename) const {
    std::ofstream out(filename);
    out << frameCollection.size() << "\n";
    for (const auto& f : frameCollection) out << f << "\n";
}

void FrameManager::importData(std::string filename) {
    std::ifstream in(filename);
    if (!in) return;
    int count;
    in >> count;
    frameCollection.clear();
    for (int i = 0; i < count; ++i) {
        TitledFrame frame;
        in >> frame;
        frameCollection.push_back(frame);
    }
}