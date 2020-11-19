import { CacheModule, Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { MathController } from './math/math.controller';
import * as redisStore from 'cache-manager-redis-store';

const getHost = () => {
  return (null == process.env.REDIS_SVC_SERVICE_HOST ? 'localhost' : process.env.REDIS_SVC_SERVICE_HOST)
}

@Module({
  imports: [ CacheModule.register({store: redisStore,
    host: getHost(),
    port: 6379,})],
  controllers: [AppController, MathController],
  providers: [AppService],
})
export class AppModule {}
