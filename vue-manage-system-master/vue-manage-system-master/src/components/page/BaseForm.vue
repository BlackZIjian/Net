<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-date"></i> 修改资料</el-breadcrumb-item>
                <el-breadcrumb-item>修改资料</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="form-box">
            <el-form ref="form" :model="form" label-width="80px">
                <el-form-item label="密码">
                    <el-input v-model="form.password"></el-input>
                </el-form-item>
                <el-form-item label="email">
                    <el-input v-model="form.email"></el-input>
                </el-form-item>
                <el-form-item label="description">
                    <el-input v-model="form.description"></el-input>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="onSubmit">提交</el-button>
                    <el-button>取消</el-button>
                </el-form-item>
            </el-form>
        </div>

    </div>
</template>

<script>
    export default {
        data: function(){
            return {
                form: {
                    password:"",
                    email:"",
                    description:""
                }
            }
        },
        methods: {
            onSubmit() {
                const  scope = this;
                $.ajax({
                    url:path + '/musers/UpdateUser',
                    type:'post',
                    data:{
                        "userName":JSON.parse(localStorage["puser"]).userName,
                        "password":this.form.password,
                        "email":this.form.email,
                        "description":this.form.description
                    },
                    success: function (res) {
                        if(res.result == "UserUpdateSuccess") {
                            localStorage["puser"] = JSON.stringify(res.user);
                            scope.$message.success('提交成功！');
                            this.$router.push('/readme');
                        }
                        else {
                            scope.$message.success(res.result);
                        }
                    },
                    error: function () {

                    }});

            }
        },
        mounted:function () {
            var user = JSON.parse(localStorage["puser"]);
            this.form.password = user.password;
            this.form.email = user.email;
            this.form.description = user.description;
        }
    }
</script>
