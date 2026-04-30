#include "FrameManager.h"
#include <iostream>
#include <string>
#include <Windows.h>

using namespace std;

void printMenu() {
    cout << "\n=== СИСТЕМА УПРАВЛІННЯ ВІКНАМИ ===" << endl;
    cout << "[1] Створити новий фрейм (на 0 позицію)" << endl;
    cout << "[2] Показати всі існуючі фрейми" << endl;
    cout << "[3] Перемістити фрейм на передній план" << endl;
    cout << "[4] Видалити фрейм за його ID" << endl;
    cout << "[5] Застосувати нову тему (зміна кольору)" << endl;
    cout << "[6] Робота з файлами (Експорт)" << endl;
    cout << "[7] Тестувати оператори + та +=" << endl;
    cout << "[0] Завершити роботу" << endl;
    cout << "==================================" << endl;
    cout << "Оберіть дію: ";
}

int main() {
    SetConsoleCP(65001);
    SetConsoleOutputCP(65001);

    FrameManager manager;
    manager.importData("frames_archive.txt"); // Автозавантаження
    int action;

    while (true) {
        printMenu();

        if (!(cin >> action)) {
            cin.clear();
            cin.ignore(10000, '\n');
            continue;
        }

        if (action == 0) {
            manager.exportData("frames_archive.txt"); // Автозбереження
            cout << "Програма завершена." << endl;
            break;
        }

        switch (action) {
        case 1: {
            int l, t, r, b;
            string bgCol, headTxt, fontCol;

            cout << "\nВведіть координати (ліва верхня X Y, права нижня X Y): ";
            cin >> l >> t >> r >> b;

            if (!UiFrame::checkBounds(l, t, r, b)) {
                cout << "Помилка: права нижня точка повинна бути більшою за ліву верхню!" << endl;
            }
            else {
                cout << "Введіть колір фону: "; cin >> bgCol;
                cout << "Введіть текст шапки: "; cin >> headTxt;
                cout << "Введіть колір шрифту: "; cin >> fontCol;

                manager.addFrame(TitledFrame(l, t, r, b, bgCol, headTxt, fontCol));
                cout << ">>> Фрейм успішно додано до списку!" << endl;
            }
            break;
        }

        case 2:
            cout << "\n--- АКТИВНІ ФРЕЙМИ ---" << endl;
            manager.showAllFrames();
            break;

        case 3: {
            int targetId;
            cout << "Вкажіть ID фрейму для фокусування: ";
            cin >> targetId;
            manager.bringToFront(targetId);
            cout << ">>> Фрейм перенесено на початок (ID 0)." << endl;
            break;
        }

        case 4: {
            int targetId;
            cout << "Вкажіть ID фрейму для видалення: ";
            cin >> targetId;
            manager.removeFrame(targetId);
            cout << ">>> Фрейм успішно видалено." << endl;
            break;
        }

        case 5: {
            string updColor;
            cout << "Введіть новий колір для масового застосування: ";
            cin >> updColor;

            for (auto it = manager.begin(); it != manager.end(); ++it) {
                UiFrame* ptr = &(*it);
                ptr->updateTheme(updColor);
            }
            cout << ">>> Тему успішно оновлено для всіх елементів." << endl;
            break;
        }

        case 6: {
            manager.exportData("frames_archive.txt");
            cout << ">>> Дані успішно збережено." << endl;
            break;
        }

        case 7: {
            cout << "\n--- Демонстрація операторів ---" << endl;
            UiFrame f1(10, 10, 40, 40, "Red");
            UiFrame f2(20, 20, 50, 50, "Blue");
            cout << "Фрейм 1: " << f1 << endl;
            cout << "Фрейм 2: " << f2 << endl;

            UiFrame f3 = f1 + f2;
            cout << "Об'єднання (спільна оболонка) через +: \n" << f3 << endl;

            cout << "\nДо зсуву: " << f1 << endl;
            f1 += 15;
            cout << "Після зсуву на +15 (через +=): " << f1 << endl;
            break;
        }

        default:
            cout << "Помилка: такого пункту меню не існує!" << endl;
            break;
        }
    }

    return 0;
}