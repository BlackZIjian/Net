<template>
    <div class="table">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-menu"></i> 访问记录</el-breadcrumb-item>
                <el-breadcrumb-item>访问记录</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="plugins-tips">
            从测试到部署后总计的访问记录
        </div>
        <datasource language="en" :table-data="getData" :columns="columns" :pagination="information.pagination"
                :actions="actions"
                v-on:change="changePage"
                v-on:searching="onSearch"></datasource>
    </div>
</template>

<script>
    import axios from 'axios';
    import Datasource from 'vue-datasource';
    export default {
        data: function(){
            const self = this;
            return {
                url: path + '/Record/GetRecords',
                information: {
                    pagination:{},
                    data:[]
                },
                columns: [
                    {
                        name: 'mrecordId',
                        key: 'mrecordId',
                    },
                    {
                        name: 'description',
                        key: 'description',
                    }
                ],
                actions: [
                    {
                        text: 'Click',
                        class: 'btn-primary',
                        event(e, row) {
                            self.$message('选中的行数： ' + row.row.id);
                        }
                    }
                ],
                query:''
            }
        },
        components: {
            Datasource
        },
        methods: {
            changePage(values) {
                this.information.pagination.per_page = values.perpage;
            },
            onSearch(searchQuery) {
                this.query = searchQuery;
            }
        },
        computed:{
            getData(){
                const self = this;
                return self.information.data;
            }
        },
        beforeMount(){

            axios.get(this.url).then( (res) => {
                console.log(res.data);
                this.information.data = res.data;
            })
        }
    }
</script>

<style src="../../../static/css/datasource.css"></style>
