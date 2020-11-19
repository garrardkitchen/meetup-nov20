import { NestFactory } from '@nestjs/core';
import { Transport, MicroserviceOptions } from '@nestjs/microservices';
import { AppModule } from './app.module';

const getHost = () => {
  return (null == process.env.MATH_SVC_SERVICE_HOST ? 'math' : process.env.MATH_SVC_SERVICE_HOST)
}

async function bootstrap() {
  const app = await NestFactory.createMicroservice<MicroserviceOptions>(
    AppModule,
    {
      transport: Transport.TCP,
      options: {        
        port: 3100
      }
    },
  );
  app.listen(() => {
    console.log('Microservice is listening')
    console.log(app);    
  });
}
bootstrap();
