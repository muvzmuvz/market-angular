import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: '',           // Главная страница
    renderMode: RenderMode.Server
  },
  {
    path: 'profile',    // Пользовательский профиль — SSR, так как данные специфичны для пользователя
    renderMode: RenderMode.Server
  },
  {
    path: 'delivery',   // Статическая страница доставки — SSG (Prerender)
    renderMode: RenderMode.Prerender
  },
  {
    path: 'cart',       // Корзина — обычно Prerender или CSR
    renderMode: RenderMode.Prerender
  },
  {
    path: 'install',    // Страница установки — Prerender
    renderMode: RenderMode.Prerender
  },
  {
    path: 'product/:id', // Динамический продукт
    renderMode: RenderMode.Server
  },
  {
    path: 'store', // Магазин
    renderMode: RenderMode.Server,
  },
  {
    path: 'store-activate',  // <--- Добавляем сюда маршрут store-activate
    renderMode: RenderMode.Server
  },
  {
    path: '**',         // Все остальные пути
    renderMode: RenderMode.Server
  }
];
