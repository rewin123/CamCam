// Podlozka.cpp: определяет экспортированные функции для приложения DLL.
//

#include "stdafx.h"
#include "Podlozka.h"
#include "ArduCamlib.h"


// Пример экспортированной переменной
PODLOZKA_API int nPodlozka=0;

// Пример экспортированной функции.
PODLOZKA_API int fnPodlozka(void)
{
    
}

// Конструктор для экспортированного класса.
// см. определение класса в Podlozka.h
CPodlozka::CPodlozka()
{
    return;
}
