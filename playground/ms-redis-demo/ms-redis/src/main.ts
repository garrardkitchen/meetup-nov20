import { NestFactory } from '@nestjs/core';
import { Transport, MicroserviceOptions } from '@nestjs/microservices';
import { AppModule } from './app.module';


const getHost = () => {
  return (null == process.env.REDIS_SVC_SERVICE_HOST ? 'math' : process.env.REDIS_SVC_SERVICE_HOST)
}

async function bootstrap() {
  const app = await NestFactory.createMicroservice<MicroserviceOptions>(
    AppModule,
    {
      transport: Transport.REDIS,
      options: {
        url: `redis://${getHost()}:6379`
      }
    },
  );
  app.listen(() => console.log('Microservice is listening'));
}
bootstrap();
