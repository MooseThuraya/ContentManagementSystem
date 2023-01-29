$(function () {
  //Sign up validation
  $("form[name='signUp']").validate({
    rules: {
      firstname: "required",
      lastname: "required",
      username: "required",
      email: {
        required: true,
        email: true
      },
      password: {
        required: true,
        minlength: 6
      },
      confirmpassword: {
        required: true,
        minlength: 6,
        equalTo: "#password",
      }
    },
    messages: {
      firstname: "* Please enter your first name",
      lastname: "* Please enter your last name",
      password: {
        required: "* Please enter a password",
        minlength: "* Your password must be at least 6 characters"
      },
      email: "* Please enter a valid email address",
      confirmpassword: "* Please enter the same password"
    },
    submitHandler: function (form) {
      form.submit();
    }
  });


  //Sign in validation
  $("form[name='signIn']").validate({
    rules: {
      email: {
        required: true,
        email: true
      },
      password: {
        required: true,
        minlength: 6
      }
    },
    messages: {
      password: {
        required: "* Please enter a password",
        minlength: "* Your password must be at least 6 characters"
      },
      email: "* Please enter a valid email address"
    },
    submitHandler: function (form) {
      form.submit();
    }
  });

  // RESET PASS
  //Sign in validation
  $("form[name='resetPass']").validate({
    rules: {
      email: {
        required: true,
        email: true
      }
    },
    messages: {
      email: "* Please enter a valid email address"
    },
    submitHandler: function (form) {
      form.submit();
    }
  });
});