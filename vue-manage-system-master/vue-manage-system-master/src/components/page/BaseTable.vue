<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i> All Teams</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="flexBox" >
            <div class="ms-doc" v-for="(group,index) in groups">
                <h3>owner:{{group.ownerName}}</h3>
                <article>
                    <h1>{{group.groupName}}</h1>
                    <h2 v-for="member in group.members">{{member.userName}}</h2>
                    <h2 style="color: #5e7382;cursor: pointer" v-on:click="JoinTeam(group.groupId)">Join</h2>
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
                groups:[],
                newGroupName:"",
                newMemberName:""
            }
        },
        methods:{
          JoinTeam:function (groupId) {
              const  scope = this;
              $.ajax({
                  url:path + '/mgroups/AddMember',
                  type:'post',
                  data:{
                      "groupId":groupId,
                      "userId":this.user.userId
                  },
                  success: function () {
                      axios.get(path + '/mgroups/GetGroup')
                          .then(function (response) {
                              let groups = response.data;
                              scope.groups = groups;
                              for(let i =0;i<groups.length;i++) {
                                  let group = groups[i];
                                  let index = i;
                                  axios.get(path + '/musers/GetUser?userId='+group.owner)
                                      .then(function (response) {
                                          scope.$set(scope.groups[index],"ownerName",response.data.user.userName);
                                      })
                                      .catch(function (error) {
                                          console.log(error);
                                      });
                                  axios.get(path + '/musers/GetUsers?groupId='+group.groupId)
                                      .then(function (response) {
                                          scope.$set(scope.groups[index],"members",response.data);
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
            axios.get(path + '/mgroups/GetGroup')
                .then(function (response) {
                    let groups = response.data;
                    scope.groups = groups;
                    for(let i =0;i<groups.length;i++) {
                        let group = groups[i];
                        let index = i;
                        axios.get(path + '/musers/GetUser?userId='+group.owner)
                            .then(function (response) {
                                scope.$set(scope.groups[index],"ownerName",response.data.user.userName);
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                        axios.get(path + '/musers/GetUsers?groupId='+group.groupId)
                            .then(function (response) {
                                scope.$set(scope.groups[index],"members",response.data);
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
