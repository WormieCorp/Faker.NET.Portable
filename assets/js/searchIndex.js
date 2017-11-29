
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"FlatHashImageFormat",
        content:"FlatHashImageFormat",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"Boolean",
        content:"Boolean",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"RoboHashImageFormat",
        content:"RoboHashImageFormat",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"PlaceholditImageFormat",
        content:"PlaceholditImageFormat",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"Config",
        content:"Config",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"App",
        content:"App",
        description:'',
        tags:''
    });

    a({
        id:6,
        title:"FlatHash",
        content:"FlatHash",
        description:'',
        tags:''
    });

    a({
        id:7,
        title:"Internet",
        content:"Internet",
        description:'',
        tags:''
    });

    a({
        id:8,
        title:"ArrayExtensions",
        content:"ArrayExtensions",
        description:'',
        tags:''
    });

    a({
        id:9,
        title:"StringExtensions",
        content:"StringExtensions",
        description:'',
        tags:''
    });

    a({
        id:10,
        title:"Phone",
        content:"Phone",
        description:'',
        tags:''
    });

    a({
        id:11,
        title:"Placeholder",
        content:"Placeholder",
        description:'',
        tags:''
    });

    a({
        id:12,
        title:"Company",
        content:"Company",
        description:'',
        tags:''
    });

    a({
        id:13,
        title:"Address",
        content:"Address",
        description:'',
        tags:''
    });

    a({
        id:14,
        title:"RandomNumber",
        content:"RandomNumber",
        description:'',
        tags:''
    });

    a({
        id:15,
        title:"Business",
        content:"Business",
        description:'',
        tags:''
    });

    a({
        id:16,
        title:"Avatar",
        content:"Avatar",
        description:'',
        tags:''
    });

    a({
        id:17,
        title:"Color",
        content:"Color",
        description:'',
        tags:''
    });

    a({
        id:18,
        title:"RoboHash",
        content:"RoboHash",
        description:'',
        tags:''
    });

    a({
        id:19,
        title:"ImageFormat",
        content:"ImageFormat",
        description:'',
        tags:''
    });

    a({
        id:20,
        title:"Name",
        content:"Name",
        description:'',
        tags:''
    });

    a({
        id:21,
        title:"NameFormats",
        content:"NameFormats",
        description:'',
        tags:''
    });

    a({
        id:22,
        title:"Code",
        content:"Code",
        description:'',
        tags:''
    });

    a({
        id:23,
        title:"Beer",
        content:"Beer",
        description:'',
        tags:''
    });

    a({
        id:24,
        title:"Lorem",
        content:"Lorem",
        description:'',
        tags:''
    });

    a({
        id:25,
        title:"EnumerableExtensions",
        content:"EnumerableExtensions",
        description:'',
        tags:''
    });

    a({
        id:26,
        title:"Date",
        content:"Date",
        description:'',
        tags:''
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/FlatHashImageFormat',
        title:"FlatHashImageFormat",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Boolean',
        title:"Boolean",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/RoboHashImageFormat',
        title:"RoboHashImageFormat",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/PlaceholditImageFormat',
        title:"PlaceholditImageFormat",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Config',
        title:"Config",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/App',
        title:"App",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/FlatHash',
        title:"FlatHash",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Internet',
        title:"Internet",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker.Extensions/ArrayExtensions',
        title:"ArrayExtensions",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker.Extensions/StringExtensions',
        title:"StringExtensions",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Phone',
        title:"Phone",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Placeholder',
        title:"Placeholder",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Company',
        title:"Company",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Address',
        title:"Address",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/RandomNumber',
        title:"RandomNumber",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Business',
        title:"Business",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Avatar',
        title:"Avatar",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Color',
        title:"Color",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/RoboHash',
        title:"RoboHash",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/ImageFormat',
        title:"ImageFormat",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Name',
        title:"Name",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/NameFormats',
        title:"NameFormats",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Code',
        title:"Code",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Beer',
        title:"Beer",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Lorem',
        title:"Lorem",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker.Extensions/EnumerableExtensions',
        title:"EnumerableExtensions",
        description:""
    });

    y({
        url:'/Faker.NET.Portable/api/Faker/Date',
        title:"Date",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
