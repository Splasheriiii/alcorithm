# Задание №3: 8,9 пункты

Консольное приложение, которое умеет делать поиск кратчайшего пути и табличку Дейкстры.

Результат выводится в консоль с разделителем **~**. Так что результат нужно вставить в любую экселеподобную программу и разделить по **~**.

[Пример результата](https://docs.google.com/spreadsheets/d/1xn0n66SPB2cV4VSrcTHH6fMoy0p8NZ5A2DjEwy5stVA/edit?usp=sharing)

## Билд

Экзешник последней версии лежит в [папке](https://github.com/Splasheriiii/alcorithm/tree/main/builds)

## Код

Уродливый, лучше не смотреть 🥲


## Аргументы командной строки

Есть два вариант передаваемых аргументов

### Кратчайший путь - №8

строка "short", затем [определение графа](##-Определение-графа)

```
.\Alcorithms.exe "short" "1,2,3,4,5,6,7,8,9,10,11,12;1-2-1,2-6-5,2-3-10,2-11-2,3-7-3,4-3-3,4-12-5,5-2-7,6-5-2,6-7-7,7-4-6,7-6-8,7-8-4,7-11-0,8-12-9,9-6-4,9-1-11,10-5-1,11-10-1,11-8-2"
```

### Дейкстра - №9

строка "dijkstra", затем номер узла для которого ищем пути, затем [определение графа](##-Определение-графа)

```
.\Alcorithms.exe "dijkstra" 6 "1,2,3,4,5,6,7,8,9,10,11,12;1-2-1,2-6-5,2-3-10,2-11-2,3-7-3,4-3-3,4-12-5,5-2-7,6-5-2,6-7-7,7-4-6,7-6-8,7-8-4,7-11-0,8-12-9,9-6-4,9-1-11,10-5-1,11-10-1,11-8-2"
```
> [!CAUTION]
> Для дейкстры неправильно возвращаются строчки для недостижимых узлов. Обязательно посмотрите на последние строчки, если они забиты нулями

## Определение графа

Для того, чтобы определить граф, нужно передать через запятую имена всех вершин. 

Затем поставить `;`. 

Затем передать все ребра в формате `a-b-c`, где: 
- a - откуда выходит ребро
- b - куда приходит ребро
- c - вес/стоимость ребра

___

`1,2,3,4,5,6,7,8,9,10,11,12;1-2-1,2-6-5,2-3-10,2-11-2,3-7-3,4-3-3,4-12-5,5-2-7,6-5-2,6-7-7,7-4-6,7-6-8,7-8-4,7-11-0,8-12-9,9-6-4,9-1-11,10-5-1,11-10-1,11-8-2` - определение графа для 1 Варианта

`0,1,2,3,4;0-1-2,0-4-10,1-2-3,1-4-7,2-3-4,3-4-8,4-2-6` - определение графа из методички, где пример для Дейкстры
