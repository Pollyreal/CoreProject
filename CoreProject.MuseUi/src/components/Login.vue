<template>
  <div>
    <mu-tabs :value="'tab1'" ><!--@change="handleTabChange"-->
      <mu-tab value="tab1" href="/login" title="Login"/>
      <mu-tab value="tab2" href="/Search" title="Search"/>
      <mu-tab value="tab3" title="TAB ACTIVE"/> <!--@active="handleActive"-->
    </mu-tabs>
    <mu-breadcrumb>
      <mu-text-field label="Account" hintText="Email or Phone" labelFloat/>
      <br/>
      <mu-text-field label="Password" hintText="Please enter password." type="password" labelFloat/>
      <br/>
      <mu-switch label="Remember Me" class="demo-switch"/>
      <br/>
      <br/>
      <mu-raised-button label="Login" class="demo-raised-button" primary/>
    </mu-breadcrumb>
  </div>
</template>
<style>
  div {
    text-align: center;
  }
</style>
<script>
  import { PngNav } from './Nav.vue'
  export default{
    components: {
      PngNav
    }
  }
</script>
