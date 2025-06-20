import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: '',           // Главная страница
    renderMode: RenderMode.Prerender
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
    path: 'cart',       // Корзина — если хранится на клиенте, можно оставить Prerender или CSR, но обычно лучше CSR
    renderMode: RenderMode.Prerender
  },
  {
    path: 'install',    // Страница установки — Prerender
    renderMode: RenderMode.Prerender
  },
  {
    path: 'product/:id', // Динамический продукт — Prerender, если продукты не меняются часто, иначе Server
    renderMode: RenderMode.Server
    // можно добавить getPrerenderParams для генерации страниц с разными id
  },
  {
    path: 'store', // Динамический продукт — Prerender, если продукты не меняются часто, иначе Server
    renderMode: RenderMode.Server,
    // можно добавить getPrerenderParams для генерации страниц с разными id
  },
  {
    path: '**',         // Все остальные пути — серверный рендеринг (SSR)
    renderMode: RenderMode.Server
  }
];
