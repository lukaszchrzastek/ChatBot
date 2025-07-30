import { UserService } from './services/user.service';
import { HealthCheckService } from './services/health-check.service';

export function initializeSequentially(
  healthCheckService: HealthCheckService,
  userService: UserService
): () => Promise<void> {
  return () => {    
    return ensureApiAvailable(healthCheckService)()
      .then(() => {                
        return ensureUserExists(userService)();
      })
      .then(() => {
        console.log('initializeSequentially OK');
      });
  };
}

export function ensureApiAvailable(healthCheckService: HealthCheckService): () => Promise<void> {
  return () =>
    new Promise((resolve, reject) => {
      console.log('ensureApiAvailable start');
      healthCheckService.checkApi().subscribe({
        next: () => {
          console.log('API dostępne');
          resolve();
        }, error: () => {
          console.log('API niedostępne, ponawianie próby...');
          setTimeout(() => {            
            healthCheckService.checkApi().subscribe({
              next: () => {
                console.log('API dostępne');
                resolve();
              }, error: () => {
                console.log('API niedostępne');
                reject(new Error('API niedostępne!'));
              }
            });            
          }, 3000);
        }
      });      
    });
}

export function ensureUserExists(userService: UserService): () => Promise<void> {
  return () =>
    new Promise((resolve, reject) => {
      console.log('ensureUserExists start');
      const userId = userService.getUserId();
      console.log(`Cookie userId: ${userId}`);

      if (!userId) {
        userService.createUser().subscribe({
          next: resolve,
          error: reject,
        });
        return;
      }

      userService.exist(userId).subscribe({
        next: (exist: boolean) => {
          console.log('Użytkownik istnieje w bazie danych');
          if (exist) {
            resolve();
          } else {
            userService.createUser().subscribe({
              next: resolve,
              error: reject,
            });
          }
        },
        error: reject,
      });
    });
}
