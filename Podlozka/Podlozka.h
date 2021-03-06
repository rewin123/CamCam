// Приведенный ниже блок ifdef - это стандартный метод создания макросов, упрощающий процедуру 
// экспорта из библиотек DLL. Все файлы данной DLL скомпилированы с использованием символа PODLOZKA_EXPORTS,
// в командной строке. Этот символ не должен быть определен в каком-либо проекте
// использующем данную DLL. Благодаря этому любой другой проект, чьи исходные файлы включают данный файл, видит 
// функции PODLOZKA_API как импортированные из DLL, тогда как данная DLL видит символы,
// определяемые данным макросом, как экспортированные.
#ifdef PODLOZKA_EXPORTS
#define PODLOZKA_API __declspec(dllexport)
#else
#define PODLOZKA_API __declspec(dllimport)
#endif

// Этот класс экспортирован из Podlozka.dll
class PODLOZKA_API CPodlozka {
public:
	CPodlozka(void);
	// TODO: Добавьте здесь свои методы.
};

extern PODLOZKA_API int nPodlozka;

PODLOZKA_API int fnPodlozka(void);
