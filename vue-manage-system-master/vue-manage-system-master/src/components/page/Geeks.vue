<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i> All Teams</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="flexBox" >
            <div class="ms-doc" v-for="(iuser,index) in users" v-on:click="clickUser(iuser.userId)">
                <article>
                    <h1>{{iuser.userName}}</h1>
                    <h3 >{{iuser.description}}</h3>
                    <h2 v-for="group in iuser.groups">{{group.groupName}}</h2>
                    <el-col :span="12">
                        <el-autocomplete
                            class="inline-input"
                            v-model="state[iuser.userId]"
                            :fetch-suggestions="querySearch"
                            placeholder="招募至"
                            :trigger-on-focus="false"
                            @select="handleSelect"
                        ></el-autocomplete>
                    </el-col>
                </article>

            </div>
        </div>


    </div>
</template>

<script>
    export default {
        data: function(){
            return {
                user:"",
                users:[],
                restaurants: [],
                state:{},
                currentUserId:0
            }
        },
        methods:{
            clickUser(userId){
                this.currentUsreId = userId;
            },
            querySearch(queryString, cb) {
                var restaurants = this.restaurants;
                var results = queryString ? restaurants.filter(this.createFilter(queryString)) : restaurants;
                // 调用 callback 返回建议列表的数据
                cb(results);
            },
            createFilter(queryString) {
                return (restaurant) => {
                    return (restaurant.value.indexOf(queryString.toLowerCase()) === 0);
                };
            },
            handleSelect(item) {
                const scope = this;
                setTimeout(function(){
                    var user = JSON.parse(localStorage["puser"]);
                    $.ajax({
                        url:path + '/mgroups/AddMember',
                        type:'post',
                        data:{
                            "groupId":item.groupId,
                            "userId":scope.currentUsreId
                        },
                        success: function () {
                            scope.restaurants = [];
                            axios.get(path + '/mgroups/GetGroup?userId='+user.userId)
                                .then(function (response) {
                                    for(var i=0;i<response.data.length;i++) {
                                        scope.restaurants.push({
                                            value: response.data[i].groupName,
                                            groupId: response.data[i].groupId
                                        });
                                    }
                                })
                                .catch(function (error) {
                                    console.log(error);
                                });
                            axios.get(path + '/musers/GetUsers')
                                .then(function (response) {
                                    let users = response.data;
                                    scope.users = users;
                                    for(let i =0;i<users.length;i++) {
                                        let iuser = users[i];
                                        let index = i;
                                        scope.state[iuser.userId] = '';
                                        axios.get(path + '/mgroups/GetGroup?userId='+iuser.userId)
                                            .then(function (response) {
                                                scope.$set(scope.users[index],"groups",response.data);
                                            })
                                            .catch(function (error) {
                                                console.log(error);
                                            });
                                    }
                                })
                                .catch(function (error) {
                                    console.log(error);
                                });
                        },
                        error: function () {

                        }
                    });
                },100)
            }
        },
        created:function () {
            var user = {
                "userName":"jllyzzj",
                "password":"jllyzzj",
                "userId":1,
                "email":"735809461@qq.com",
                "description":"software developer"
            };
            user = JSON.parse(localStorage["puser"]);
            const scope = this;
            this.user = user;
            axios.get(path + '/mgroups/GetGroup?userId='+user.userId)
                .then(function (response) {
                    for(var i=0;i<response.data.length;i++) {
                        scope.restaurants.push({
                            value: response.data[i].groupName,
                            groupId: response.data[i].groupId
                        });
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
            axios.get(path + '/musers/GetUsers')
                .then(function (response) {
                    let users = response.data;
                    scope.users = users;
                    for(let i =0;i<users.length;i++) {
                        let iuser = users[i];
                        let index = i;
                        scope.state[iuser.userId] = '';
                        axios.get(path + '/mgroups/GetGroup?userId='+iuser.userId)
                            .then(function (response) {
                                scope.$set(scope.users[index],"groups",response.data);
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }
</script>

<style scoped>
    .flexBox{
        display: flex;
        flex-wrap: wrap;
    }
    .h1Input{
        font-size:32px;
        padding-bottom: 10px;
        margin-bottom: 15px;
        border-bottom: 1px solid #ddd;
        width: 80%;
    }
    .addMember {
        margin: 24px 0 16px;
        font-weight: 600;
        line-height: 1.25;
        padding-bottom: 7px;
        font-size: 24px;
        border-bottom: 1px solid #eee;
        width: 80%;
    }
    .ms-doc{
        width:20%;
        padding-left: 10%;
        padding-top: 50px;
        max-width: 980px;
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Helvetica, Arial, sans-serif;
    }
    .ms-doc h3{
        padding: 9px 10px 10px;
        margin: 0;
        font-size: 14px;
        line-height: 17px;
        background-color: #f5f5f5;
        border: 1px solid #d8d8d8;
        border-bottom: 0;
        border-radius: 3px 3px 0 0;
    }
    .ms-doc article{
        padding: 45px;
        word-wrap: break-word;
        background-color: #fff;
        border: 1px solid #ddd;
        border-bottom-right-radius: 3px;
        border-bottom-left-radius: 3px;
    }
    .ms-doc article h1{
        font-size:32px;
        padding-bottom: 10px;
        margin-bottom: 15px;
        border-bottom: 1px solid #ddd;
    }
    .ms-doc article h2 {
        margin: 24px 0 16px;
        font-weight: 600;
        line-height: 1.25;
        padding-bottom: 7px;
        font-size: 24px;
        border-bottom: 1px solid #eee;
    }
    .ms-doc article p{
        margin-bottom: 15px;
        line-height: 1.5;
    }
    .ms-doc article .el-checkbox{
        margin-bottom: 5px;
    }
</style>
