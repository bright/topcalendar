# Architektura kalendarzyka i wzorce #

Polecam przeczytać ten artykuł, wzorce z mvp,mvc,mvvm w przystępnej formie zebrane w jednym miejscu w kontekście WPF.


**[Patterns For Building Composite Applications With WPF](http://msdn.microsoft.com/pl-pl/magazine/cc785479(en-us).aspx)**



Myślę, że warto było by umieszczać wszelkie nowinki projektowe na wiki, miedzy innymi w tym miejscu ;]



---


# Architektura modułu #

Pojedyncze moduły mogą komunikować się tylko z fasadami w projekcie TopCalendar.Client.Connector + przez eventy. Z zewnątrz jedyne odwołanie jest z Bootstrapera, docelowo z managera pluginów (bo coś musi wywołać klasę definiującą moduł).

Na schemacie wpisane jest z grubsza co poszczególne klasy robią. Strzałka oznacza "korzysta z".

![http://www.lotosgdansk.pl/tmp/TopCalendarModule.png](http://www.lotosgdansk.pl/tmp/TopCalendarModule.png)

Do obsługi GUI moduły powinny:
  * nasłuchiwać na eventy, na które chcą się wyświetlić
  * w reakcji na event dopisać się do regionu za pomocą IPluginLoader.RegisterViewWithRegion(string regionName, IView view) - nazwy regionów są zdefiniowane w RegionNames i realizowane w XAML-u
  * jeśli chcą się usunąć z GUI - powinny opublikować event UnloadModuleEvent z obiektem IView jako parametr