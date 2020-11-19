import { CacheInterceptor, CacheTTL, Controller, UseInterceptors } from '@nestjs/common';
import { MessagePattern } from '@nestjs/microservices';

@Controller('math')
export class MathController {
    @CacheTTL(10)
    @UseInterceptors(CacheInterceptor)
    @MessagePattern({cmd: 'sum'})
    accumulate(data: number[]) : number {
        console.log(data)
        return (data || []).reduce((a,b) => a+b)
    }
}
