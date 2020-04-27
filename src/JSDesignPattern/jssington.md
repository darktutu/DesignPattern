JS 单例模式
设计模式javascript
更新于 2019-07-13   约 9 分钟
1. 单例模式
单例模式 (Singleton) 的实现在于保证一个特定类只有一个实例，第二次使用同一个类创建新对象的时候，应该得到与第一次创建对象完全相同的对象。
当创建一个新对象时，实际上没有其他对象与其类似，因为新对象已经是单例了 {a:1} === {a:1} // false 。

但是如何在对构造函数使用 new 操作符创建多个对象的时候仅获取一个单例对象呢。

2. 静态属性中的实例
在构造函数的静态属性中缓存该实例，缺点在于 instance 属性是公开可访问的属性，在外部代码中可能会修改该属性。

function Universe() {
    if (typeof Universe.instance === 'object') {        // 判断是否已经有单例了
        return Universe.instance
    }
    Universe.instance = this
    return this
}
var uni1 = new Universe()
var uni2 = new Universe()
uni1 === uni2            // true
3. 闭包中的实例
可以把实例封装在闭包中，这样可以保证该实例的私有性并且保证该实例不会在构造函数之外被修改，代价是带来了额外的闭包开销。

function Universe() {
    var instance = this
    Universe = function() {    // 重写构造函数
        return instance
    }
}
var uni1 = new Universe()
var uni2 = new Universe()
uni1 === uni2         // true
当第一次调用构造函数时，它正常返回 this ，然后在以后调用时，它将会执行重写构造函数，这个构造函数通过闭包访问了私有 instance 变量，并且简单的返回了该 instance。

4. 惰性单例
有时候对于单例对象需要延迟创建，所以在单例中还存在一种延迟创建的形式，也有人称之为惰性创建。

const LazySingle = (function() {
  let _instance              // 单例的实例引用
 
  function Single() {        // 单例构造函数
    const desc = '单例'        // 私有属性和方法
    return {                   // 暴露出来的对象
      publicMethod: function() {console.log(desc)},
      publickProperty: '1.0'
    }
  }
  
  return function() {
    return _instance || (_instance = Single())
  }
})()

console.log(LazySingle()===lazySingle())        // true
console.log(LazySingle().publickProperty)       // 1.0
5. 改进
之前在构造函数中重写自身会丢失所有在初始定义和重定义之间添加到其中的属性。在这种情况下，任何添加到 Universe() 的原型中的对象都不会存在指向由原始实现所创建实例的活动链接：

function Universe() {
    var instance = this
    Universe = function() {
        return instance
    }
}
Universe.prototype.nothing = true
var uni1 = new Universe()
Universe.prototype.enthing = true
var uni2 = new Universe()
console.log(uni1 === uni2) // true

uni1.nothing // true
uni2.nothing // true
uni1.enthing // undefined
uni2.enthing // undefined
uni1.constructor.name // "Universe"
uni1.constructor === Universe // false
之所以 uni1.constructor 不再与 Universe() 相同，是因为uni1.constructor仍然指向原始的构造函数，而不是重定义之后的那个构造函数。
可以通过一些调整实现原型和构造函数指针按照预期的那样运行：

function Universe() {
    var instance
    Universe = function Universe() {
        return instance
    }
    Universe.prototype = this // 保留原型属性
    instance = new Universe()
    instance.constructor = Universe // 重置构造函数指针
    instance.start_time = 0 // 一些属性
    instance.big = 'yeah'
    return instance
}
Universe.prototype.nothing = true
var uni1 = new Universe()
Universe.prototype.enthing = true
var uni2 = new Universe()
console.log(uni1 === uni2) // true

uni1.nothing & uni2.nothing & uni1.enthing & uni2.enthing // true
uni1.constructor.name // "Universe"
uni1.constructor === Universe // true
uni1.big    // "yeah"
uni2.big    // "yeah"
本文是系列文章，可以相互参考印证，共同进步~

JS 抽象工厂模式
JS 工厂模式
JS 建造者模式
JS 原型模式
JS 单例模式
JS 回调模式
JS 外观模式
JS 适配器模式
JS 利用高阶函数实现函数缓存(备忘模式)
JS 状态模式
JS 桥接模式
JS 观察者模式
网上的帖子大多深浅不一，甚至有些前后矛盾，在下的文章都是学习过程中的总结，如果发现错误，欢迎留言指出~

参考：
《JavaScript模式》 P143
《Javascript 设计模式》 - 张荣铭
设计模式之单例模式